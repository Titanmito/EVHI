using System;
using System.IO;

public class GlobalVariables
{
    public static int nbFeatures = 46;
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
        initPath = "users_data_init_level.csv";
    public static string csvValueSeparator = ",", csvLineSeparator = "\n";
    public static string cultureName = "fr-FR";

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
                id = Int32.Parse(line.Split(csvValueSeparator[0])[0]);
                if (i == 1)
                    max = id;
                else if (id > max)
                    max = id;
            }
        }
        return max;
    }
}
