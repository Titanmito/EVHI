using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GlobalVariables
{
    public static int nbFeatures = 46;
    public static ushort levelCount = 3;
    public static ushort keysCount = 6;
    public static ushort nbExtraFeatures = 3;
    public static string featuresExceptionMessage =
        "The length of the given features array must correspond exactly to " +
        "NbFeatures";
    public static string[] databaseFeaturesColumnOrder =
    {
        "v_cross", "v_square", "v_triangle", "v_circle", "v_l1", "v_r1", "v_number",
        "v_cross_cross", "v_cross_square", "v_cross_triangle", "v_cross_circle",
        "v_cross_l1", "v_cross_r1", "v_square_cross", "v_square_square", "v_square_triangle",
        "v_square_circle", "v_square_l1", "v_square_r1", "v_triangle_cross",
        "v_triangle_square", "v_triangle_triangle", "v_triangle_circle", "v_triangle_l1",
        "v_triangle_r1", "v_circle_cross", "v_circle_square", "v_circle_triangle",
        "v_circle_circle", "v_circle_l1", "v_circle_r1", "v_l1_cross", "v_l1_square",
        "v_l1_triangle", "v_l1_circle", "v_l1_l1", "v_l1_r1", "v_r1_cross", "v_r1_square",
        "v_r1_triangle", "v_r1_circle", "v_r1_l1", "v_r1_r1", "v_mental", "v_pointing", "v_typing"
    };
    public static string usersPath = "users.csv", archivePath = "users_data_archive.csv",
        initPath = "users_data_init_level.csv", mentalPointingTypingPath = "mental_pointing_typing_traces.csv",
        buttonPushPath = "button_push_traces.csv";
    public static string lastAptitudeRequest = "last_aptitude_request.csv", lastAptitudeReply = "last_aptitude_reply.txt";
    public static int csvUsersIDIndex = 0, csvUsersNameIndex = 1, csvUsersCurrentLevelIndex = 2,
        csvUsersAptitudeIndex = 3, csvUsersFeaturesFirstIndex = 4, csvUsersFeaturesLastIndex = 50;
    public static int csvInitIDIndex = 0, csvInitAptitudeIndex = 1, csvInitFeaturesFirstIndex = 2,
        csvInitFeaturesLastIndex = 48;
    public static int csvArchiveIDIndex = 0, csvArchiveAptitudeIndex = 1,
        csvArchiveFeaturesFirstIndex = 2, csvArchiveFeaturesLastIndex = 48, csvArchiveDateIndex = 48;
    public static string csvValueSeparator = ",", csvLineSeparator = "\n";
    public static string cultureName = "fr-FR";
    public static string goNameUsersCreator = "UsersCreator", goNameUsersManager = "UsersManager",
        goNameJoystickManager = "JoystickManager", goNameButtonPushContainer = "ButtonPushContainer";
    public static string levelTitlePrefix = "Niveau", levelTitleSep = " ", levelTitleTestPostfix = "test";
    public static string dualshock4Name = "Dualshock 4";
    public static readonly Dictionary<JoystickButton, KeyCode> dualshock4Buttons = new Dictionary<JoystickButton, KeyCode>
    {
        {JoystickButton.Square, KeyCode.JoystickButton0},
        {JoystickButton.Cross, KeyCode.JoystickButton1},
        {JoystickButton.Circle, KeyCode.JoystickButton2},
        {JoystickButton.Triangle, KeyCode.JoystickButton3},
        {JoystickButton.L1, KeyCode.JoystickButton4},
        {JoystickButton.R1, KeyCode.JoystickButton5}
    };
    public static string thrustmasterESwapProContollerName = "Thrustmaster eSwap Pro controller";
    public static readonly Dictionary<JoystickButton, KeyCode> thrustmasterESwapProContollerButtons = new Dictionary<JoystickButton, KeyCode>
    {
        {JoystickButton.Cross, KeyCode.JoystickButton0},
        {JoystickButton.Circle, KeyCode.JoystickButton1},
        {JoystickButton.Square, KeyCode.JoystickButton2},
        {JoystickButton.Triangle, KeyCode.JoystickButton3},
        {JoystickButton.L1, KeyCode.JoystickButton4},
        {JoystickButton.R1, KeyCode.JoystickButton5}
    };
    // (Level, aptitude) -> LevelDifficulty
    public static readonly Dictionary<Tuple<int, ushort>, LevelDifficulty> levelDifficulties = new Dictionary<Tuple<int, ushort>, LevelDifficulty>
    {
        {Tuple.Create(0, (ushort)0), new LevelDifficulty(0.8f, 30)},

        {Tuple.Create(1, (ushort)0), new LevelDifficulty(1.6f, 26)},
        {Tuple.Create(1, (ushort)1), new LevelDifficulty(1.3f, 28)},
        {Tuple.Create(1, (ushort)2), new LevelDifficulty(1.0f, 30)},
        {Tuple.Create(1, (ushort)3), new LevelDifficulty(0.7f, 32)},
        {Tuple.Create(1, (ushort)4), new LevelDifficulty(0.4f, 34)},

        {Tuple.Create(2, (ushort)0), new LevelDifficulty(1.5f, 29)},
        {Tuple.Create(2, (ushort)1), new LevelDifficulty(1.2f, 32)},
        {Tuple.Create(2, (ushort)2), new LevelDifficulty(0.9f, 35)},
        {Tuple.Create(2, (ushort)3), new LevelDifficulty(0.6f, 38)},
        {Tuple.Create(2, (ushort)4), new LevelDifficulty(0.3f, 41)},

        {Tuple.Create(3, (ushort)0), new LevelDifficulty(1.4f, 32)},
        {Tuple.Create(3, (ushort)1), new LevelDifficulty(1.1f, 36)},
        {Tuple.Create(3, (ushort)2), new LevelDifficulty(0.8f, 40)},
        {Tuple.Create(3, (ushort)3), new LevelDifficulty(0.5f, 44)},
        {Tuple.Create(3, (ushort)4), new LevelDifficulty(0.2f, 48)}
    };

    public static int GetMaxUserID()
    {
        string[] lines = File.ReadAllLines(usersPath);
        string line = "";
        int max = -1, id = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            line = lines[i];
            if (line != "")
            {
                id = Int32.Parse(line.Split(csvValueSeparator[0])[csvUsersIDIndex]);
                if (i == 1)
                    max = id;
                else if (id > max)
                    max = id;
            }
        }
        return max;
    }

    public enum JoystickButton
    {
        Cross, Square, Triangle, Circle, L1, R1, L2, R2, L3, R3, Left, Right, Down, Up, Share, Options, PS, Touchpad
    }

    public enum JoystickModel
    {
        Dualshock4, ThrustmasterESwapProContoller
    }

    public static readonly KeyCode[] JoystickKeys = new KeyCode[6]
    {
        KeyCode.JoystickButton0,
        KeyCode.JoystickButton1,
        KeyCode.JoystickButton2,
        KeyCode.JoystickButton3,
        KeyCode.JoystickButton4,
        KeyCode.JoystickButton5
    };

    public static class DictionaryUtiles<TKey, TValue>
    {
        public static TKey GetKey(Dictionary<TKey, TValue> dictionary, TValue value)
        {
            if (!dictionary.ContainsValue(value))
                throw new InvalidOperationException("Value not found in the dictionary!");
            return dictionary.FirstOrDefault(x => x.Value.Equals(value)).Key;
        }
    }
}
