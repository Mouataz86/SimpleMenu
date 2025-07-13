using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using LemonUI;
using LemonUI.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

public class SimpleMenu : Script
{
    private Keys toggleMenuKey = Keys.F10; // fallback
    private Dictionary<string, List<VehicleData>> vehicleCategories;
    private string vehicleDataPath = Path.Combine("scripts", "SM Data", "vehicles.json");
    private ObjectPool menuPool;
    private NativeMenu mainMenu;
    private NativeMenu playerMenu;
    private NativeMenu vehicleMenu;
    private NativeMenu wantedMenu;
    private NativeMenu settingsMenu;
    private NativeMenu weaponMenu;
    private NativeMenu teleportMenu;
    private NativeMenu worldMenu;
    private NativeMenu carTuningMenu;
    private NativeMenu vehiclePropertiesMenu;
    private NativeMenu spawnVehicleMenu;
    private NativeMenu playerAbilitiesMenu;
    private NativeMenu playerVisionMenu;

    private NativeListItem<string> tintList;
    private NativeListItem<int> hourList;
    private NativeListItem<int> minuteList;
    private NativeItem applyTimeItem;
    private NativeListItem<string> weatherList;
    private NativeItem applyWeatherItem;
    private NativeCheckboxItem freezeTimeItem;
    private NativeListItem<string> timeOfDayList;
    private NativeListItem<string> weatherTypeList;
    private NativeCheckboxItem flamingBulletsItem;
    private NativeCheckboxItem explosiveMeleeItem;
    private NativeCheckboxItem superPunchItem;
    private NativeCheckboxItem superJumpItem;
    private NativeCheckboxItem fastRunItem;
    private NativeCheckboxItem fastSwimItem;
    private NativeCheckboxItem ragdollToggleItem;
    private NativeCheckboxItem drunkModeItem;
    private NativeCheckboxItem invisiblePlayerItem;
    private NativeCheckboxItem bulletproofTiresItem;
    private NativeCheckboxItem turboToggleItem;
    private NativeCheckboxItem stickToGroundItem;
    private NativeCheckboxItem autoRepairItem;
    private NativeCheckboxItem autoFlipItem;
    private NativeCheckboxItem engineAlwaysOnItem;
    private NativeCheckboxItem preventEjectionItem;
    private NativeCheckboxItem nightVisionItem;
    private NativeCheckboxItem thermalVisionItem;
    private NativeCheckboxItem motionBlurItem;
    private NativeCheckboxItem cinematicBarsItem;
    private NativeCheckboxItem cameraShakeItem;


    private Dictionary<string, Tuple<int, int, int>> neonColors = new Dictionary<string, Tuple<int, int, int>>()
{
    { "White", Tuple.Create(255, 255, 255) },
    { "Blue", Tuple.Create(0, 0, 255) },
    { "Electric Blue", Tuple.Create(0, 150, 255) },
    { "Mint Green", Tuple.Create(50, 255, 155) },
    { "Lime Green", Tuple.Create(0, 255, 0) },
    { "Yellow", Tuple.Create(255, 255, 0) },
    { "Golden Shower", Tuple.Create(204, 204, 0) },
    { "Orange", Tuple.Create(255, 128, 0) },
    { "Red", Tuple.Create(255, 0, 0) },
    { "Pony Pink", Tuple.Create(255, 102, 255) },
    { "Hot Pink", Tuple.Create(255, 0, 255) },
    { "Purple", Tuple.Create(153, 0, 204) },
    { "Blacklight", Tuple.Create(15, 3, 255) }
};

    private NativeListItem<string> primaryColorList;
    private NativeListItem<string> secondaryColorList;
    private NativeCheckboxItem neonLightsItem;
    private NativeListItem<string> neonColorList;
    private NativeListItem<string> wheelTypeList;
    private NativeListItem<int> frontWheelsList;
    private NativeListItem<int> spoilerModList;
    private NativeListItem<int> bumperModList;
    private NativeListItem<int> exhaustModList;



    private NativeItem setMaxHealthArmorItem;
    private NativeCheckboxItem displayStatsItem;

    private bool displayPlayerStats = false;

    private bool freezeTime = false;
    private int frozenTime = 0;

    private bool turboBoostEnabled = false; //For Turbo Boost Toggle

    private NativeMenu npcMenu;
    private NativeListItem<string> trafficDensityList;
    private NativeListItem<string> pedestrianDensityList;
    private NativeCheckboxItem ignorePlayerItem;
    private NativeCheckboxItem chaosModeItem;

    private bool chaosEnabled = false;
    private int chaosTimer = 0;



    private NativeCheckboxItem godModeItem;
    private NativeCheckboxItem infiniteAbilityItem;
    private NativeItem healPlayerItem;

    private NativeCheckboxItem vehicleGodModeItem;
    private NativeItem repairVehicleItem;
    private NativeItem destroyVehicleItem;

    private NativeListItem<int> wantedLevelList;
    private NativeCheckboxItem neverWantedItem;
    private NativeItem clearWantedItem;
    private NativeCheckboxItem freezeWantedItem;

    private NativeItem resetMenuItem;
    private NativeItem saveConfigItem;
    private NativeItem loadConfigItem;

    NativeItem versionItem = new NativeItem("Version: 1.0");
    NativeItem authorItem = new NativeItem("Author: MO3IZO");
    NativeItem githubItem = new NativeItem("https://github.com/Mouataz86/");
    NativeItem gta5ModsItem = new NativeItem("https://www.gta5-mods.com/users/Mouataz");
    NativeItem contactItem = new NativeItem("Contact: https://t.me/MO3IZO");

    private NativeItem giveAllWeaponsItem;
    private NativeCheckboxItem infiniteAmmoItem;
    private NativeItem giveMaxAmmoItem;
    private NativeCheckboxItem freezeAmmoItem;
    private NativeItem removeAllWeaponsItem;

    private NativeItem teleportObjectiveItem;
    private NativeItem teleportWaypointItem;
    private NativeItem getPersonalCarItem;
    private NativeItem teleportToCarItem;

    private Vehicle personalCar;
    private Blip personalCarBlip; // <-- If any
    private float currentTrafficDensity = 1f;
    private float currentPedDensity = 1f;

    private bool infiniteAbilityEnabled = false;
    private bool freezeWantedEnabled = false;
    private bool neverWantedEnabled = false;
    private bool infiniteAmmoEnabled = false;
    private bool freezeAmmoEnabled = false;
    private bool menuVisible = false;

    public SimpleMenu()
    {
        // Load toggle key before assigning KeyDown handler
        string iniKey = IniReader.Read("Menu", "OpenKey", "F10");
        if (!Enum.TryParse(iniKey, out Keys parsedKey))
        {
            toggleMenuKey = Keys.F10;
            Notification.Show("~r~Invalid OpenKey in INI. Defaulting to F10.");
        }
        else
        {
            toggleMenuKey = parsedKey;
            Notification.Show($"~g~SimpleMenu loaded succesfully! With Open  Key: {toggleMenuKey}");
        }

        Tick += OnTick;
        KeyDown += OnKeyDown;

        if (!Directory.Exists("SM Data"))
            Directory.CreateDirectory("SM Data");

        menuPool = new ObjectPool();
        //To load the JSON file so our list of cars will be ready
        if (File.Exists(vehicleDataPath))
        {
            string json = File.ReadAllText(vehicleDataPath);
            vehicleCategories = JsonConvert.DeserializeObject<Dictionary<string, List<VehicleData>>>(json);
        }
        else
        {
            vehicleCategories = new Dictionary<string, List<VehicleData>>();
            Notification.Show("~r~Missing vehicles.json in SM Data folder!");
        }


        mainMenu = new NativeMenu("SimpleMenu", "Made by MO3IZO");
        playerMenu = new NativeMenu("Player Settings", "Manage player features");
        playerAbilitiesMenu = new NativeMenu("Abilities", "Player Movement and Effects");
        playerVisionMenu = new NativeMenu("Player Vision", "Special vision modes");
        vehicleMenu = new NativeMenu("Vehicle Settings", "Manage vehicle features");
        carTuningMenu = new NativeMenu("Car Tuning", "Visuals & Upgrades");
        vehiclePropertiesMenu = new NativeMenu("Utility Features", "Vehicle Behavior & Toggles");
        spawnVehicleMenu = new NativeMenu("Spawn Vehicle", "Choose a vehicle to spawn");
        wantedMenu = new NativeMenu("Wanted Level Settings", "Manage wanted level");
        settingsMenu = new NativeMenu("Menu Settings", "Customize the menu");
        weaponMenu = new NativeMenu("Weapon Settings", "Manage weapons");
        teleportMenu = new NativeMenu("Teleport", "Teleport Options");
        worldMenu = new NativeMenu("World Settings", "Manage time and weather");
        npcMenu = new NativeMenu("NPC Settings", "Manage world NPC behavior");
        NativeMenu aboutMenu = new NativeMenu("About", "Information & Credits");

        mainMenu.AddSubMenu(playerMenu);
        playerMenu.AddSubMenu(playerAbilitiesMenu);
        playerMenu.AddSubMenu(playerVisionMenu);
        mainMenu.AddSubMenu(vehicleMenu);
        vehicleMenu.AddSubMenu(carTuningMenu);
        vehicleMenu.AddSubMenu(vehiclePropertiesMenu);
        vehicleMenu.AddSubMenu(spawnVehicleMenu);
        mainMenu.AddSubMenu(wantedMenu);
        mainMenu.AddSubMenu(weaponMenu);
        mainMenu.AddSubMenu(teleportMenu);
        mainMenu.AddSubMenu(worldMenu);
        mainMenu.AddSubMenu(npcMenu);
        mainMenu.AddSubMenu(settingsMenu);
        mainMenu.AddSubMenu(aboutMenu);

        aboutMenu.Add(versionItem);
        aboutMenu.Add(authorItem);
        aboutMenu.Add(githubItem);
        aboutMenu.Add(gta5ModsItem);
        aboutMenu.Add(contactItem);

        menuPool.Add(mainMenu);
        menuPool.Add(playerMenu);
        menuPool.Add(playerAbilitiesMenu);
        menuPool.Add(playerVisionMenu);
        menuPool.Add(vehicleMenu);
        menuPool.Add(carTuningMenu);
        menuPool.Add(vehiclePropertiesMenu);
        menuPool.Add(spawnVehicleMenu);
        menuPool.Add(wantedMenu);
        menuPool.Add(weaponMenu);
        menuPool.Add(teleportMenu);
        menuPool.Add(worldMenu);
        menuPool.Add(npcMenu);
        menuPool.Add(settingsMenu);
        menuPool.Add(aboutMenu);


        // Player settings
        godModeItem = new NativeCheckboxItem("God Mode", false);
        infiniteAbilityItem = new NativeCheckboxItem("Infinite Ability", false);
        healPlayerItem = new NativeItem("Heal Player");
        setMaxHealthArmorItem = new NativeItem("Set Max Health & Armor");
        displayStatsItem = new NativeCheckboxItem("Display Health & Armor", false);
        // Player Abilities Group
        flamingBulletsItem = new NativeCheckboxItem("Flaming Bullets", false);
        explosiveMeleeItem = new NativeCheckboxItem("Explosive Melee", false);
        superPunchItem = new NativeCheckboxItem("Super Punch", false);
        superJumpItem = new NativeCheckboxItem("Super Jump", false);
        fastRunItem = new NativeCheckboxItem("Fast Run", false);
        fastSwimItem = new NativeCheckboxItem("Fast Swim", false);
        ragdollToggleItem = new NativeCheckboxItem("Disable Ragdoll", false);
        drunkModeItem = new NativeCheckboxItem("Drunk Mode", false);
        invisiblePlayerItem = new NativeCheckboxItem("Invisible Player", false);

        //Player Vision Mod
        nightVisionItem = new NativeCheckboxItem("Night Vision", false);
        thermalVisionItem = new NativeCheckboxItem("Thermal Vision", false);
        motionBlurItem = new NativeCheckboxItem("Motion Blur", false);
        cinematicBarsItem = new NativeCheckboxItem("Cinematic Black Bars", false);
        cameraShakeItem = new NativeCheckboxItem("Camera Shake", false);

        playerMenu.Add(godModeItem);
        playerMenu.Add(infiniteAbilityItem);
        playerMenu.Add(healPlayerItem);
        playerMenu.Add(setMaxHealthArmorItem);
        playerMenu.Add(displayStatsItem);
        playerMenu.Add(invisiblePlayerItem);

        playerAbilitiesMenu.Add(superPunchItem);
        playerAbilitiesMenu.Add(superJumpItem);
        playerAbilitiesMenu.Add(fastRunItem);
        playerAbilitiesMenu.Add(fastSwimItem);
        playerAbilitiesMenu.Add(ragdollToggleItem);
        playerAbilitiesMenu.Add(drunkModeItem);

        playerVisionMenu.Add(nightVisionItem);
        playerVisionMenu.Add(thermalVisionItem);
        playerVisionMenu.Add(motionBlurItem);
        playerVisionMenu.Add(cinematicBarsItem);
        playerVisionMenu.Add(cameraShakeItem);

        // Vehicle settings
        vehicleGodModeItem = new NativeCheckboxItem("Vehicle God Mode", false);
        repairVehicleItem = new NativeItem("Repair Vehicle");
        destroyVehicleItem = new NativeItem("Destroy Vehicle Engine");
        NativeItem maxUpgradeItem = new NativeItem("Max Upgrade Vehicle");
        bulletproofTiresItem = new NativeCheckboxItem("Bulletproof Tires", true); //Trust me it's good to keep it active you'll need it
        tintList = new NativeListItem<string>("Window Tint", new[]
        {
    "None", "Pure Black", "Dark Smoke", "Light Smoke", "Stock", "Limo", "Green"
        });
        primaryColorList = new NativeListItem<string>("Primary Color", new[] {
    "Black", "White", "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "Gold", "Chrome"
        });
        secondaryColorList = new NativeListItem<string>("Secondary Color", primaryColorList.Items.ToArray());

        neonLightsItem = new NativeCheckboxItem("Enable Neon Lights", false);
        neonColorList = new NativeListItem<string>("Neon Color", new[] {
    "White", "Blue", "Electric Blue", "Mint Green", "Lime Green", "Yellow", "Golden Shower",
    "Orange", "Red", "Pony Pink", "Hot Pink", "Purple", "Blacklight"
});

        wheelTypeList = new NativeListItem<string>("Wheel Type", new[] {
    "Sport", "Muscle", "Lowrider", "SUV", "Offroad", "Tuner", "Motorcycle", "High End"
});

        turboToggleItem = new NativeCheckboxItem("Enable Turbo Boost", false);
        stickToGroundItem = new NativeCheckboxItem("Stick to Ground", false);
        autoRepairItem = new NativeCheckboxItem("Auto Repair Vehicle", false);
        autoFlipItem = new NativeCheckboxItem("Auto Flip Vehicle", false);
        engineAlwaysOnItem = new NativeCheckboxItem("Engine Always On", false);
        preventEjectionItem = new NativeCheckboxItem("Prevent Ejection", false);

        frontWheelsList = new NativeListItem<int>("Front Wheels", new[] { 0, 1, 2, 3, 4, 5, 6, 7 });

        spoilerModList = new NativeListItem<int>("Spoiler Mod", new[] { 0, 1, 2, 3, 4 });
        bumperModList = new NativeListItem<int>("Front Bumper Mod", new[] { 0, 1, 2, 3 });
        exhaustModList = new NativeListItem<int>("Exhaust Mod", new[] { 0, 1, 2, 3 });

        //Spawn Cars Part


        vehicleMenu.Add(vehicleGodModeItem);
        vehicleMenu.Add(repairVehicleItem);
        vehicleMenu.Add(destroyVehicleItem);
        carTuningMenu.Add(maxUpgradeItem);
        carTuningMenu.Add(bulletproofTiresItem);
        carTuningMenu.Add(tintList);
        carTuningMenu.Add(primaryColorList);
        carTuningMenu.Add(secondaryColorList);
        carTuningMenu.Add(neonLightsItem);
        carTuningMenu.Add(neonColorList);
        carTuningMenu.Add(wheelTypeList);
        carTuningMenu.Add(frontWheelsList);
        carTuningMenu.Add(spoilerModList);
        carTuningMenu.Add(bumperModList);
        carTuningMenu.Add(exhaustModList);
        vehiclePropertiesMenu.Add(turboToggleItem);
        vehiclePropertiesMenu.Add(stickToGroundItem);
        vehiclePropertiesMenu.Add(autoRepairItem);
        vehiclePropertiesMenu.Add(autoFlipItem);
        vehiclePropertiesMenu.Add(engineAlwaysOnItem);
        vehiclePropertiesMenu.Add(preventEjectionItem);

        //Spawn Cars Part

        foreach (var category in vehicleCategories)
        {
            NativeMenu categoryMenu = new NativeMenu(category.Key, $"Spawn a {category.Key} vehicle");

            foreach (var veh in category.Value)
            {
                NativeItem item = new NativeItem(veh.name);
                item.Activated += (s, e) =>
                {
                    Ped player = Game.Player.Character;
                    Vector3 spawnPos = player.Position + player.ForwardVector * 5f;

                    VehicleHash hash = (VehicleHash)Function.Call<int>(Hash.GET_HASH_KEY, veh.hash);
                    Vehicle vehicle = World.CreateVehicle(hash, spawnPos);

                    if (vehicle != null && vehicle.Exists())
                    {
                        vehicle.PlaceOnGround();
                        player.SetIntoVehicle(vehicle, VehicleSeat.Driver);
                        Notification.Show($"~g~Spawned {veh.name}");
                    }
                    else
                    {
                        Notification.Show($"~r~Failed to spawn {veh.name}");
                    }
                };

                categoryMenu.Add(item);
            }

            spawnVehicleMenu.AddSubMenu(categoryMenu);
            menuPool.Add(categoryMenu);
        }

        // Wanted settings
        wantedLevelList = new NativeListItem<int>("Wanted Level", new[] { 0, 1, 2, 3, 4, 5 });
        wantedLevelList.SelectedIndex = Game.Player.WantedLevel;

        neverWantedItem = new NativeCheckboxItem("Never Wanted", false);
        clearWantedItem = new NativeItem("Clear Wanted Level");
        freezeWantedItem = new NativeCheckboxItem("Freeze Wanted Level", false);

        wantedMenu.Add(wantedLevelList);
        wantedMenu.Add(neverWantedItem);
        wantedMenu.Add(clearWantedItem);
        wantedMenu.Add(freezeWantedItem);

        // Menu settings
        resetMenuItem = new NativeItem("Reset Menu");
        saveConfigItem = new NativeItem("Save Changes");
        loadConfigItem = new NativeItem("Load Changes");

        settingsMenu.Add(resetMenuItem);
        settingsMenu.Add(saveConfigItem);
        settingsMenu.Add(loadConfigItem);

        // Weapon settings
        giveAllWeaponsItem = new NativeItem("Give All Upgraded Weapons");
        infiniteAmmoItem = new NativeCheckboxItem("Infinite Ammo Clip", false);
        giveMaxAmmoItem = new NativeItem("Give Max Ammo");
        freezeAmmoItem = new NativeCheckboxItem("Freeze Ammo", false);
        removeAllWeaponsItem = new NativeItem("Remove All Weapons");

        weaponMenu.Add(giveAllWeaponsItem);
        weaponMenu.Add(infiniteAmmoItem);
        weaponMenu.Add(giveMaxAmmoItem);
        weaponMenu.Add(freezeAmmoItem);
        weaponMenu.Add(explosiveMeleeItem);
        weaponMenu.Add(flamingBulletsItem);
        weaponMenu.Add(removeAllWeaponsItem);

        // Teleport settings
        teleportObjectiveItem = new NativeItem("Teleport To Mission Objective");
        teleportWaypointItem = new NativeItem("Teleport Map Point");
        getPersonalCarItem = new NativeItem("Get Quick Fully Upgraded Car");
        teleportToCarItem = new NativeItem("Teleport to Upgraded Car");

        teleportMenu.Add(teleportObjectiveItem);
        teleportMenu.Add(teleportWaypointItem);
        teleportMenu.Add(getPersonalCarItem);
        teleportMenu.Add(teleportToCarItem);

        // World Settings
        hourList = new NativeListItem<int>("Hour", new[] {
    0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
    12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23
});

        minuteList = new NativeListItem<int>("Minute", new[] { 0, 15, 30, 45 });

        applyTimeItem = new NativeItem("Apply Time");

        freezeTimeItem = new NativeCheckboxItem("Freeze Time", false);

        timeOfDayList = new NativeListItem<string>("Time of Day", new[] {
    "Midnight", "Morning", "Noon", "Evening"
});

        weatherTypeList = new NativeListItem<string>("Weather Type", new[] {
    "CLEAR", "EXTRASUNNY", "CLOUDS", "OVERCAST", "RAIN",
    "CLEARING", "THUNDER", "SMOG", "FOGGY", "XMAS",
    "SNOWLIGHT", "BLIZZARD"
});

        applyWeatherItem = new NativeItem("Apply Weather");

        // Add to worldMenu
        worldMenu.Add(hourList);
        worldMenu.Add(minuteList);
        worldMenu.Add(applyTimeItem);
        // Removed `weatherList` (null) and replaced with `weatherTypeList` - I got this error from SHVDN3.log
        worldMenu.Add(weatherTypeList);
        worldMenu.Add(applyWeatherItem);
        worldMenu.Add(freezeTimeItem);
        worldMenu.Add(timeOfDayList);


        // NPC Settings
        trafficDensityList = new NativeListItem<string>("Traffic Density", new[] { "None", "Low", "Normal", "High" });
        pedestrianDensityList = new NativeListItem<string>("Pedestrian Density", new[] { "None", "Low", "Normal", "High" });
        ignorePlayerItem = new NativeCheckboxItem("Everyone Ignores Player", false);
        chaosModeItem = new NativeCheckboxItem("Enable Chaos Mode", false);

        npcMenu.Add(trafficDensityList);
        npcMenu.Add(pedestrianDensityList);
        npcMenu.Add(ignorePlayerItem);
        npcMenu.Add(chaosModeItem);

        // Events



        setMaxHealthArmorItem.Activated += (s, e) =>
        {
            Ped player = Game.Player.Character;
            player.MaxHealth = 200;
            player.Health = 200;
            player.Armor = 100;
            Notification.Show("~g~Max Health & Armor Set (200/100)");
        };

        displayStatsItem.CheckboxChanged += (s, e) =>
        {
            displayPlayerStats = displayStatsItem.Checked;
            Notification.Show(displayPlayerStats ? "~b~Player Stats HUD Enabled" : "~r~Player Stats HUD Disabled");
        };

        githubItem.Activated += (s, e) =>
        {
            Notification.Show("~b~GitHub: github.com/Mouataz86/SimpleMenu");
        };

        gta5ModsItem.Activated += (s, e) =>
        {
            Notification.Show("~y~GTA5-Mods.com: https://www.gta5-mods.com/users/Mouataz");
        };

        contactItem.Activated += (s, e) =>
        {
            Notification.Show("~g~Contact At Telegram: https://t.me/MO3IZO");
        };

        authorItem.Activated += (s, e) =>
        {
            Notification.Show("~g~Author: If you want you can support me at paypal: contact@mo3izo-tech.tk.");
        };

        maxUpgradeItem.Activated += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh == null || !veh.Exists())
            {
                Notification.Show("~r~You are not in a vehicle.");
                return;
            }

            Function.Call(Hash.SET_VEHICLE_MOD_KIT, veh, 0);

            // Loop through all 50 mod slots (safe upper limit)
            for (int i = 0; i <= 49; i++)
            {
                int modCount = Function.Call<int>(Hash.GET_NUM_VEHICLE_MODS, veh, i);
                if (modCount > 0)
                {
                    Function.Call(Hash.SET_VEHICLE_MOD, veh, i, modCount - 1, false);
                }
            }

            // Enable toggle mods (turbo, xenon lights, etc)
            for (int i = 17; i <= 22; i++) // Common toggle ranges
            {
                if (Function.Call<bool>(Hash.IS_TOGGLE_MOD_ON, veh, i) || Function.Call<int>(Hash.GET_NUM_VEHICLE_MODS, veh, i) > 0)
                {
                    Function.Call(Hash.TOGGLE_VEHICLE_MOD, veh, i, true);
                }
            }

            // Wheel + tire + plate + tint
            Function.Call(Hash.SET_VEHICLE_WHEEL_TYPE, veh, 7); // High-end
            Function.Call(Hash.SET_VEHICLE_MOD, veh, 23, 0, true); // Front wheels
            Function.Call(Hash.SET_VEHICLE_TYRES_CAN_BURST, veh, false); // Bulletproof
            Function.Call(Hash.SET_VEHICLE_WINDOW_TINT, veh, 1); // Light smoke
            Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT, veh, "TUNEDUP");
            Function.Call(Hash.SET_VEHICLE_COLOURS, veh, 158, 158); // Gold color
            Function.Call(Hash.TOGGLE_VEHICLE_MOD, veh, 18, true);

            Notification.Show("~g~All possible upgrades applied to your vehicle.");
        };

        tintList.ItemChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null && veh.Exists())
            {
                Function.Call(Hash.SET_VEHICLE_WINDOW_TINT, veh, tintList.SelectedIndex);
                Notification.Show($"~b~Window Tint set to: {tintList.SelectedItem}");
            }
        };

        turboToggleItem.CheckboxChanged += (s, e) =>
        {
            turboBoostEnabled = turboToggleItem.Checked;
            Notification.Show(turboBoostEnabled ? "~g~Turbo Boost Enabled! Press E to activate." : "~r~Turbo Boost Disabled.");
        };

        bulletproofTiresItem.CheckboxChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null && veh.Exists())
            {
                Function.Call(Hash.SET_VEHICLE_TYRES_CAN_BURST, veh, !bulletproofTiresItem.Checked);
                Notification.Show(bulletproofTiresItem.Checked ? "~g~Bulletproof tires enabled." : "~r~Tires can now burst.");
            }
        };

        primaryColorList.ItemChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null && veh.Exists())
            {
                OutputArgument outPrimary = new OutputArgument();
                OutputArgument outSecondary = new OutputArgument();
                Function.Call(Hash.GET_VEHICLE_COLOURS, veh, outPrimary, outSecondary);
                int secondary = outSecondary.GetResult<int>();

                int primary = GetColorIndex(primaryColorList.SelectedItem);
                Function.Call(Hash.SET_VEHICLE_COLOURS, veh, primary, secondary);
                Notification.Show($"~b~Primary color set to {primaryColorList.SelectedItem}");
            }
        };

        secondaryColorList.ItemChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null && veh.Exists())
            {
                OutputArgument outPrimary = new OutputArgument();
                OutputArgument outSecondary = new OutputArgument();
                Function.Call(Hash.GET_VEHICLE_COLOURS, veh, outPrimary, outSecondary);
                int primary = outPrimary.GetResult<int>();

                int secondary = GetColorIndex(secondaryColorList.SelectedItem);
                Function.Call(Hash.SET_VEHICLE_COLOURS, veh, primary, secondary);
                Notification.Show($"~b~Secondary color set to {secondaryColorList.SelectedItem}");
            }
        };


        neonLightsItem.CheckboxChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh == null || !veh.Exists())
            {
                Notification.Show("~r~You are not in a vehicle.");
                return;
            }

            bool enabled = neonLightsItem.Checked;

            // Enable or disable neon lights on all 4 sides
            for (int i = 0; i < 4; i++)
            {
                Function.Call((Hash)0x2AA720E4287BF269, veh, i, enabled); // SET_VEHICLE_NEON_LIGHT_ENABLED
            }

            if (enabled)
            {
                // Apply current selected neon color when toggled on
                Vector3 color = GetNeonColor(neonColorList.SelectedItem);
                Function.Call((Hash)0x8E0A582209A62695, veh, (int)color.X, (int)color.Y, (int)color.Z); // SET_VEHICLE_NEON_LIGHTS_COLOUR
                Notification.Show("~b~Neon lights enabled.");
            }
            else
            {
                Notification.Show("~r~Neon lights disabled.");
            }
        };
        neonColorList.ItemChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh == null || !veh.Exists())
            {
                Notification.Show("~r~You are not in a vehicle.");
                return;
            }

            Vector3 color = GetNeonColor(neonColorList.SelectedItem);
            Function.Call((Hash)0x8E0A582209A62695, veh, (int)color.X, (int)color.Y, (int)color.Z); // SET_VEHICLE_NEON_LIGHTS_COLOUR
            Notification.Show($"~b~Neon color set to {neonColorList.SelectedItem}");
        };


        wheelTypeList.ItemChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null && veh.Exists())
            {
                int index = wheelTypeList.SelectedIndex;
                Function.Call(Hash.SET_VEHICLE_WHEEL_TYPE, veh, index);
                Notification.Show($"~b~Wheel type set to {wheelTypeList.SelectedItem}");
            }
        };
        frontWheelsList.ItemChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null && veh.Exists())
            {
                Function.Call(Hash.SET_VEHICLE_MOD, veh, 23, frontWheelsList.SelectedItem, true);
                Notification.Show($"~b~Front wheels mod set to {frontWheelsList.SelectedItem}");
            }
        };
        spoilerModList.ItemChanged += (s, e) =>
        {
            SetVehicleMod(0, spoilerModList.SelectedItem, "Spoiler");
        };
        bumperModList.ItemChanged += (s, e) =>
        {
            SetVehicleMod(1, bumperModList.SelectedItem, "Front Bumper");
        };
        exhaustModList.ItemChanged += (s, e) =>
        {
            SetVehicleMod(4, exhaustModList.SelectedItem, "Exhaust");
        };


        godModeItem.CheckboxChanged += (s, e) =>
        {
            Game.Player.Character.IsInvincible = godModeItem.Checked;
            Notification.Show($"~b~God Mode: {(godModeItem.Checked ? "Enabled" : "Disabled")}");
        };

        infiniteAbilityItem.CheckboxChanged += (s, e) =>
        {
            infiniteAbilityEnabled = infiniteAbilityItem.Checked;
            Notification.Show($"~b~Infinite Ability: {(infiniteAbilityEnabled ? "Enabled" : "Disabled")}");
        };

        healPlayerItem.Activated += (s, e) =>
        {
            Ped player = Game.Player.Character;
            player.Health = player.MaxHealth;
            player.Armor = 100;
            Notification.Show("~g~Healed!");
        };

        vehicleGodModeItem.CheckboxChanged += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null)
            {
                veh.IsInvincible = vehicleGodModeItem.Checked;
                Notification.Show($"~b~Vehicle God Mode: {(vehicleGodModeItem.Checked ? "Enabled" : "Disabled")}");
            }
            else
            {
                Notification.Show("~r~You are not in a vehicle.");
            }
        };

        repairVehicleItem.Activated += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null)
            {
                veh.Repair();
                Notification.Show("~g~Vehicle Repaired!");
            }
            else
            {
                Notification.Show("~r~You're not in a vehicle.");
            }
        };

        destroyVehicleItem.Activated += (s, e) =>
        {
            Vehicle veh = Game.Player.Character.CurrentVehicle;
            if (veh != null)
            {
                veh.EngineHealth = -4000;
                Notification.Show("~r~Vehicle Engine Destroyed!");
            }
            else
            {
                Notification.Show("~r~You're not in a vehicle.");
            }
        };

        wantedLevelList.ItemChanged += (s, e) =>
        {
            if (!neverWantedEnabled)
            {
                Game.Player.WantedLevel = wantedLevelList.SelectedItem;
                Notification.Show($"~y~Wanted Level set to {wantedLevelList.SelectedItem}");
            }
        };

        clearWantedItem.Activated += (s, e) =>
        {
            Game.Player.WantedLevel = 0;
            Notification.Show("~g~Wanted Level Cleared!");
        };

        neverWantedItem.CheckboxChanged += (s, e) =>
        {
            neverWantedEnabled = neverWantedItem.Checked;
            if (neverWantedEnabled)
            {
                Game.Player.WantedLevel = 0;
                Notification.Show("~g~Never Wanted Enabled");
            }
            else
            {
                Notification.Show("~y~Never Wanted Disabled");
            }
        };

        freezeWantedItem.CheckboxChanged += (s, e) =>
        {
            freezeWantedEnabled = freezeWantedItem.Checked;
            Notification.Show($"~b~Freeze Wanted: {(freezeWantedEnabled ? "Enabled" : "Disabled")}");
        };

        //Player Vision
        nightVisionItem.CheckboxChanged += (s, e) =>
        {
            Function.Call(Hash.SET_NIGHTVISION, nightVisionItem.Checked);
            Notification.Show($"~b~Night Vision {(nightVisionItem.Checked ? "Enabled" : "Disabled")}");
        };

        thermalVisionItem.CheckboxChanged += (s, e) =>
        {
            Function.Call(Hash.SET_SEETHROUGH, thermalVisionItem.Checked);
            Notification.Show($"~o~Thermal Vision {(thermalVisionItem.Checked ? "Enabled" : "Disabled")}");
        };

        motionBlurItem.CheckboxChanged += (s, e) =>
        {
            if (motionBlurItem.Checked)
            {
                Function.Call(Hash.SET_TIMECYCLE_MODIFIER, "scanline_cam_cheap");
            }
            else
            {
                Function.Call(Hash.CLEAR_TIMECYCLE_MODIFIER);
            }

            Notification.Show($"~c~Motion Blur {(motionBlurItem.Checked ? "Enabled" : "Disabled")}");
        };



        cinematicBarsItem.CheckboxChanged += (s, e) =>
        {
            Function.Call(Hash.SET_CINEMATIC_BUTTON_ACTIVE, !cinematicBarsItem.Checked); // prevent interference
            Function.Call(Hash.SET_CINEMATIC_MODE_ACTIVE, cinematicBarsItem.Checked);
            Notification.Show($"~c~Cinematic Bars {(cinematicBarsItem.Checked ? "Enabled" : "Disabled")}");
        };

        cameraShakeItem.CheckboxChanged += (s, e) =>
        {
            if (cameraShakeItem.Checked)
            {
                Function.Call(Hash.SHAKE_GAMEPLAY_CAM, "DRUNK_SHAKE", 0.5f); // Lower intensity
            }
            else
            {
                Function.Call(Hash.STOP_GAMEPLAY_CAM_SHAKING, true);
            }

            Notification.Show($"~b~Camera Shake {(cameraShakeItem.Checked ? "Enabled" : "Disabled")}");
        };


        resetMenuItem.Activated += (s, e) =>
        {
            godModeItem.Checked = false;
            infiniteAbilityItem.Checked = false;
            vehicleGodModeItem.Checked = false;
            neverWantedItem.Checked = false;
            freezeWantedItem.Checked = false;
            wantedLevelList.SelectedIndex = 0;
            infiniteAmmoItem.Checked = false;
            freezeAmmoItem.Checked = false;
            Notification.Show("~r~Menu Reset Complete!");
        };

        saveConfigItem.Activated += (s, e) =>
        {
            Notification.Show("~g~Settings saved! (INI support coming soon)");
        };


        loadConfigItem.Activated += (s, e) =>
        {
            Notification.Show("~g~Settings loaded! (INI support coming soon)");
        };




        giveAllWeaponsItem.Activated += (s, e) =>
        {
            Ped player = Game.Player.Character;

            foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
            {
                if (weapon == WeaponHash.Unarmed) continue;

                var w = player.Weapons.Give(weapon, 9999, false, true);
                Function.Call(Hash.ADD_AMMO_TO_PED, player, (uint)weapon, 9999);

                try { w.Tint = WeaponTint.Gold; } catch { }

                foreach (WeaponComponent comp in w.Components)
                {
                    try { comp.Active = true; } catch { }
                }
            }

            Notification.Show("~g~All upgraded weapons granted!");
        };

        infiniteAmmoItem.CheckboxChanged += (s, e) =>
        {
            infiniteAmmoEnabled = infiniteAmmoItem.Checked;
            Notification.Show($"~b~Infinite Ammo Clip: {(infiniteAmmoEnabled ? "Enabled" : "Disabled")}");
        };

        giveMaxAmmoItem.Activated += (s, e) =>
        {
            Ped player = Game.Player.Character;
            foreach (Weapon weapon in player.Weapons)
            {
                if (weapon.Hash != WeaponHash.Unarmed)
                {
                    Function.Call(Hash.ADD_AMMO_TO_PED, player, (uint)weapon.Hash, 9999);
                }
            }
            Notification.Show("~g~All weapons refilled with Max ammo.");
        };

        freezeAmmoItem.CheckboxChanged += (s, e) =>
        {
            freezeAmmoEnabled = freezeAmmoItem.Checked;
            Notification.Show($"~b~Freeze Ammo: {(freezeAmmoEnabled ? "Enabled" : "Disabled")}");
        };

        removeAllWeaponsItem.Activated += (s, e) =>
        {
            Game.Player.Character.Weapons.RemoveAll();
            Notification.Show("~r~All weapons removed.");
        };

        teleportObjectiveItem.Activated += (s, e) =>
        {
            Vector3 pos = GetWaypointPosition();
            if (pos != Vector3.Zero)
            {
                Game.Player.Character.Position = pos;
                Notification.Show("~g~Teleported to mission objective!");
            }
            else
            {
                Notification.Show("~r~No waypoint set!");
            }
        };

        teleportWaypointItem.Activated += (s, e) =>
        {
            Vector3 pos = GetWaypointPosition();
            if (pos != Vector3.Zero)
            {
                Game.Player.Character.Position = pos;
                Notification.Show("~g~Teleported to waypoint!");
            }
            else
            {
                Notification.Show("~r~No waypoint set!");
            }
        };

        getPersonalCarItem.Activated += (s, e) =>
        {
            Ped player = Game.Player.Character;
            VehicleHash fastestCar = VehicleHash.Adder;

            // Delete old car if it exists
            if (personalCar != null && personalCar.Exists())
            {
                personalCar.Delete();
            }

            // Spawn new vehicle
            personalCar = World.CreateVehicle(fastestCar, player.Position.Around(5));
            personalCar.IsPersistent = true;
            personalCar.PlaceOnGround();

            // Put player inside
            player.Task.WarpIntoVehicle(personalCar, VehicleSeat.Driver);

            // Custom appearance
            Function.Call(Hash.SET_VEHICLE_COLOURS, personalCar, 158, 158); // Gold metallic
            Function.Call(Hash.SET_VEHICLE_WINDOW_TINT, personalCar, 1); // Tinted windows
            Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT, personalCar, "QUICKCAR");

            // Mods
            Function.Call(Hash.SET_VEHICLE_MOD_KIT, personalCar, 0);
            Function.Call(Hash.SET_VEHICLE_WHEEL_TYPE, personalCar, 7); // High-end wheels
            Function.Call(Hash.SET_VEHICLE_MOD, personalCar, 11, 3, false); // Engine
            Function.Call(Hash.SET_VEHICLE_MOD, personalCar, 12, 2, false); // Brakes
            Function.Call(Hash.SET_VEHICLE_MOD, personalCar, 13, 2, false); // Transmission
            Function.Call(Hash.SET_VEHICLE_MOD, personalCar, 15, 3, false); // Suspension
            Function.Call(Hash.SET_VEHICLE_MOD, personalCar, 16, 4, false); // Armor
            Function.Call(Hash.TOGGLE_VEHICLE_MOD, personalCar, 18, true);  // Turbo

            // Blip
            Blip blip = personalCar.AddBlip();
            blip.Sprite = BlipSprite.PersonalVehicleCar;
            blip.Color = BlipColor.Red;
            blip.Name = "Quick Car";
            blip.IsFlashing = true;

            Notification.Show("~g~Spawned a fully upgraded Adder near you.");
        };


        teleportToCarItem.Activated += (s, e) =>
        {
            if (personalCar != null && personalCar.Exists())
            {
                Game.Player.Character.Position = personalCar.Position + new Vector3(2, 2, 0);
                Notification.Show("~g~Teleported to your car.");
            }
            else
            {
                Notification.Show("~r~Personal car not found.");
            }
        };

        applyTimeItem.Activated += (s, e) =>
        {
            World.CurrentTimeOfDay = new TimeSpan(hourList.SelectedItem, minuteList.SelectedItem, 0);
            Notification.Show($"Time set to {hourList.SelectedItem:D2}:{minuteList.SelectedItem:D2}");
        };

        applyWeatherItem.Activated += (s, e) =>
        {
            Function.Call(Hash.SET_WEATHER_TYPE_NOW_PERSIST, weatherList.SelectedItem);
            Function.Call(Hash.CLEAR_WEATHER_TYPE_PERSIST);
            Notification.Show($"Weather set to {weatherList.SelectedItem}");
        };

        trafficDensityList.ItemChanged += (s, e) =>
        {
            float value = 1f;
            switch (trafficDensityList.SelectedItem)
            {
                case "None": value = 0f; break;
                case "Low": value = 0.25f; break;
                case "Normal": value = 1f; break;
                case "High": value = 2f; break;
            }

            // Store current density to apply every frame in OnTick
            currentTrafficDensity = value;

            Notification.Show($"~b~Traffic Density set to: {trafficDensityList.SelectedItem}");
        };

        pedestrianDensityList.ItemChanged += (s, e) =>
        {
            float value = 1f;
            switch (pedestrianDensityList.SelectedItem)
            {
                case "None": value = 0f; break;
                case "Low": value = 0.25f; break;
                case "Normal": value = 1f; break;
                case "High": value = 2f; break;
            }

            currentPedDensity = value;

            Notification.Show($"~b~Pedestrian Density set to: {pedestrianDensityList.SelectedItem}");
        };


        ignorePlayerItem.CheckboxChanged += (s, e) =>
        {
            Function.Call(Hash.SET_EVERYONE_IGNORE_PLAYER, Game.Player, ignorePlayerItem.Checked);
            Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, Game.Player, ignorePlayerItem.Checked);
            Notification.Show(ignorePlayerItem.Checked ? "~y~Everyone now ignores you." : "~r~NPCs will react to you again.");
        };

        chaosModeItem.CheckboxChanged += (s, e) =>
        {
            chaosEnabled = chaosModeItem.Checked;
            Notification.Show(chaosEnabled ? "~r~CHAOS MODE ACTIVATED!" : "~g~Chaos Mode Disabled.");
        };

        freezeTimeItem.CheckboxChanged += (s, e) =>
        {
            freezeTime = freezeTimeItem.Checked;
            if (freezeTime)
            {
                frozenTime = World.CurrentTimeOfDay.Hours;
                Notification.Show("~b~Time frozen.");
            }
            else
            {
                Notification.Show("~g~Time resumed.");
            }
        };

        timeOfDayList.ItemChanged += (s, e) =>
        {
            switch (timeOfDayList.SelectedItem)
            {
                case "Midnight": World.CurrentTimeOfDay = new TimeSpan(0, 0, 0); break;
                case "Morning": World.CurrentTimeOfDay = new TimeSpan(9, 0, 0); break;
                case "Noon": World.CurrentTimeOfDay = new TimeSpan(12, 0, 0); break;
                case "Evening": World.CurrentTimeOfDay = new TimeSpan(18, 0, 0); break;
            }
            Notification.Show($"~y~Time set to: {timeOfDayList.SelectedItem}");
        };




        weatherTypeList.ItemChanged += (s, e) =>
{
    Function.Call(Hash.SET_WEATHER_TYPE_NOW_PERSIST, weatherTypeList.SelectedItem);
    Function.Call(Hash.SET_OVERRIDE_WEATHER, weatherTypeList.SelectedItem);
    Notification.Show($"~b~Weather set to: {weatherTypeList.SelectedItem}");
};



    }

    private void SetVehicleMod(int modType, int modIndex, string modName)
    {
        Vehicle veh = Game.Player.Character.CurrentVehicle;
        if (veh != null && veh.Exists())
        {
            Function.Call(Hash.SET_VEHICLE_MOD_KIT, veh, 0);
            Function.Call(Hash.SET_VEHICLE_MOD, veh, modType, modIndex, false);
            Notification.Show($"~b~{modName} set to {modIndex}");
        }
    }


    //Available Colors ( You can increase the cases depends on your choice )
    private int GetColorIndex(string name)
    {
        switch (name)
        {
            case "Black": return 0;
            case "White": return 111;
            case "Red": return 27;
            case "Blue": return 64;
            case "Green": return 55;
            case "Yellow": return 88;
            case "Orange": return 38;
            case "Purple": return 145;
            case "Pink": return 135;
            case "Gold": return 158;
            case "Chrome": return 120;
            default: return 0;
        }
    }

    //Neon Colors
    private Vector3 GetNeonColor(string name)
    {
        switch (name)
        {
            case "White": return new Vector3(255, 255, 255);
            case "Blue": return new Vector3(0, 0, 255);
            case "Electric Blue": return new Vector3(0, 150, 255);
            case "Mint Green": return new Vector3(50, 255, 155);
            case "Lime Green": return new Vector3(0, 255, 0);
            case "Yellow": return new Vector3(255, 255, 0);
            case "Golden Shower": return new Vector3(255, 220, 0);
            case "Orange": return new Vector3(255, 140, 0);
            case "Red": return new Vector3(255, 0, 0);
            case "Pony Pink": return new Vector3(255, 102, 255);
            case "Hot Pink": return new Vector3(255, 0, 150);
            case "Purple": return new Vector3(150, 0, 255);
            case "Blacklight": return new Vector3(15, 3, 255);
            default: return new Vector3(255, 255, 255);
        }
    }

    private Vector3 GetWaypointPosition()
    {
        foreach (Blip b in World.GetAllBlips())
        {
            if (b.Exists() && b.Sprite == BlipSprite.Waypoint)
            {
                return b.Position;
            }
        }
        return Vector3.Zero;
    }

    private void OnTick(object sender, EventArgs e)
    {
        menuPool.Process();

        if (infiniteAbilityEnabled)
        {
            Function.Call(Hash.SPECIAL_ABILITY_FILL_METER, Game.Player);
        }

        if (neverWantedEnabled && Game.Player.WantedLevel > 0)
        {
            Game.Player.WantedLevel = 0;
        }

        Ped player = Game.Player.Character;

        // Abilities
        if (flamingBulletsItem.Checked)
        {
            Function.Call(Hash.SET_FIRE_AMMO_THIS_FRAME, Game.Player); //This was not a call I added call to prevent flaming bullet from being active
        }
        if (explosiveMeleeItem.Checked)
        {
            Function.Call(Hash.SET_EXPLOSIVE_MELEE_THIS_FRAME, Game.Player);
        }
        Function.Call(Hash.SET_RUN_SPRINT_MULTIPLIER_FOR_PLAYER, Game.Player, fastRunItem.Checked ? 1.49f : 1.0f);
        Function.Call(Hash.SET_SWIM_MULTIPLIER_FOR_PLAYER, Game.Player, fastSwimItem.Checked ? 1.49f : 1.0f);

        player.CanRagdoll = !ragdollToggleItem.Checked;
        if (drunkModeItem.Checked)
        {
            if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@drunk@verydrunk"))
            {
                Function.Call(Hash.REQUEST_ANIM_SET, "move_m@drunk@verydrunk");
            }
            else
            {
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, player, "move_m@drunk@verydrunk", 1.0f);
                Function.Call(Hash.SET_PED_IS_DRUNK, player, true);
                Function.Call(Hash.SET_TIME_SCALE, 0.95f);

                // Shake camera (intensity can be lowered if needed)
                if (!Function.Call<bool>(Hash.IS_GAMEPLAY_CAM_SHAKING))
                {
                    Function.Call(Hash.SHAKE_GAMEPLAY_CAM, "DRUNK_SHAKE", 1.0f); // Range 0.0 - 1.0
                }
            }
        }
        else
        {
            Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, player, 1.0f);
            Function.Call(Hash.SET_PED_IS_DRUNK, player, false);
            Function.Call(Hash.SET_TIME_SCALE, 1.0f);
            Function.Call(Hash.STOP_GAMEPLAY_CAM_SHAKING, true);
        }

        //Punch Force
        if (superPunchItem.Checked)
        {
            player = Game.Player.Character;

            // Increase unarmed weapon damage
            Function.Call(Hash.SET_WEAPON_DAMAGE_MODIFIER, (uint)WeaponHash.Unarmed, 10.0f); // 10x normal damage
        }
        else
        {
            // Reset to normal damage when disabled
            Function.Call(Hash.SET_WEAPON_DAMAGE_MODIFIER, (uint)WeaponHash.Unarmed, 1.0f);
        }





        player.IsVisible = !invisiblePlayerItem.Checked;


        if (freezeWantedEnabled)
        {
            Game.Player.WantedLevel = wantedLevelList.SelectedItem;
        }

        if (infiniteAmmoEnabled)
        {
            Game.Player.Character.Weapons.Current.InfiniteAmmoClip = true;
        }
        else
        {
            Game.Player.Character.Weapons.Current.InfiniteAmmoClip = false;
        }

        Vehicle veh = Game.Player.Character.CurrentVehicle;

        if (veh != null && veh.Exists())
        {
            if (turboToggleItem.Checked)
                Function.Call(Hash.TOGGLE_VEHICLE_MOD, veh, 18, true); // Turbo

            if (stickToGroundItem.Checked)
                Function.Call(Hash.SET_VEHICLE_ON_GROUND_PROPERLY, veh);

            if (autoRepairItem.Checked && veh.Health < veh.MaxHealth)
                veh.Repair();

            if (autoFlipItem.Checked && veh.IsUpsideDown)
                veh.PlaceOnGround();

            if (engineAlwaysOnItem.Checked)
                veh.IsEngineRunning = true;

            if (preventEjectionItem.Checked)
                Function.Call(Hash.SET_PED_CAN_BE_KNOCKED_OFF_VEHICLE, Game.Player.Character, 1); // Prevents ejection
        }


        if (freezeAmmoEnabled)
        {
            var currentWeapon = Game.Player.Character.Weapons.Current;
            if (currentWeapon != null)
            {
                currentWeapon.Ammo = currentWeapon.MaxAmmo;
            }
        }

        // Apply traffic density every frame
        Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, currentTrafficDensity);
        Function.Call(Hash.SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, currentTrafficDensity);
        Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, currentTrafficDensity);

        // Apply pedestrian density every frame
        Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, currentPedDensity);
        Function.Call(Hash.SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME, currentPedDensity, currentPedDensity);

        // Chaos Mode logic (explosions, fire, panic)
        if (chaosEnabled)
        {
            if (Game.GameTime > chaosTimer)
            {
                Vector3 pos = Game.Player.Character.Position.Around(20f);

                // Explosion
                Function.Call(Hash.ADD_EXPLOSION, pos.X, pos.Y, pos.Z, 2, 5f, true, false, 0f);

                // Fire (use START_ENTITY_FIRE or simulate fire with explosions)
                Function.Call(Hash.ADD_EXPLOSION, pos.X + 2f, pos.Y, pos.Z, 12 /* FIRE */, 3f, true, false, 0f);

                // Drunk movement just for fun
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Game.Player.Character, "move_m@drunk@verydrunk", 1f);

                chaosTimer = Game.GameTime + 2500;
            }
        }

        if (cameraShakeItem != null && cameraShakeItem.Checked)
        {
            Function.Call(Hash.SHAKE_GAMEPLAY_CAM, "ROAD_VIBRATION_SHAKE", 0.5f);
        }

        if (cinematicBarsItem != null && cinematicBarsItem.Checked)
        {
            Function.Call(Hash.SET_WIDESCREEN_BORDERS, true, 0); // Show black bars
        }
        else
        {
            Function.Call(Hash.SET_WIDESCREEN_BORDERS, false, 0); // Hide black bars
        }


        // Despawn QuickCar if destroyed, player dead, in cutscene, or in interior
        if (personalCar != null && personalCar.Exists())
            if (personalCar != null && personalCar.Exists())
            {
                // Check if player is in interior
                bool inInterior = Function.Call<int>(Hash.GET_INTERIOR_FROM_ENTITY, Game.Player.Character) != 0;

                // Check if a cutscene is active
                bool isCutscene = Function.Call<bool>(Hash.IS_CUTSCENE_ACTIVE);

                if (personalCar.IsDead || !Game.Player.Character.IsAlive || inInterior || isCutscene)
                {
                    // Remove the tracked blip
                    personalCarBlip?.Delete();
                    personalCarBlip = null;

                    // Delete the vehicle safely
                    personalCar.MarkAsNoLongerNeeded();
                    personalCar.Delete();
                    personalCar = null;
                }
            }
        // Freeze Time
        if (freezeTime)
        {
            TimeSpan currentTime = World.CurrentTimeOfDay;
            World.CurrentTimeOfDay = new TimeSpan(frozenTime, currentTime.Minutes, currentTime.Seconds);
        }
        //This part for the counter display
        if (displayPlayerStats)
        {
            Ped ped = Game.Player.Character;
            int hp = player.Health;
            int maxHp = player.MaxHealth;
            int armor = player.Armor;

            string statsText = $"~b~HP:~w~ {hp}/{maxHp}  ~g~Armor:~w~ {armor}";
            GTA.UI.Screen.ShowSubtitle(statsText, 1);
        }
        if (drunkModeItem.Checked)
        {
            Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Game.Player.Character, "move_m@drunk@verydrunk", 1.0f);
        }
        else
        {
            Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, Game.Player.Character, 1.0f);
        }
        if (superJumpItem.Checked)
        {
            Function.Call(Hash.SET_SUPER_JUMP_THIS_FRAME, Game.Player.Handle);
        }

    }
    //Vehicle Spawn Define
    public class VehicleData
    {
        public string name { get; set; }
        public string hash { get; set; }
    }

    public static class IniReader
    {
        private static string iniPath = "scripts/SimpleMenu.ini";

        public static string Read(string section, string key, string defaultValue)
        {
            if (!File.Exists(iniPath))
                return defaultValue;

            string[] lines = File.ReadAllLines(iniPath);
            string currentSection = "";

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line.Substring(1, line.Length - 2);
                }
                else if (currentSection == section && line.Contains("="))
                {
                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts[0].Trim() == key)
                        return parts[1].Trim();
                }
            }

            return defaultValue;
        }
    }


    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == toggleMenuKey)
        {
            menuVisible = !menuVisible;

            foreach (var menu in menuPool)
            {
                if (menu is NativeMenu nativeMenu)
                {
                    nativeMenu.Visible = menuVisible && nativeMenu == mainMenu;
                }
            }
        }

        if (e.KeyCode == Keys.E && turboBoostEnabled) //Default Turbo Key " E "
        {
            Ped player = Game.Player.Character;
            if (player.IsInVehicle())
            {
                Vehicle veh = player.CurrentVehicle;
                if (veh != null && veh.Exists())
                {
                    Vector3 direction = veh.ForwardVector;
                    veh.Velocity += direction * 10f; //Turbo Power
                    Notification.Show("~b~Turbo Boost Activated!"); //Turbo Notification
                }
            }
        }
    }
}
