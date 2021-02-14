using System;
using System.Linq;
using System.IO;
using System.Globalization;

public class User
{
    private int id;
    private string name;
    private double[] features, initFeatures;
    private ushort aptitude, initAptitude;
    private ushort currentLevel;

    public int UserID { get => id; }
    public string Name { get => name; set => name = value; }
    public ushort Aptitude { get => aptitude; set => aptitude = value; }
    public ushort InitAptitude { get => initAptitude; set => initAptitude = value; }
    public double[] Features { get => features; set => UpdateFeatures(value); }
    public double[] InitFeatures { get => initFeatures; set => UpdateFeatures(value, initFeatures: true); }
    public ushort CurrentLevel { get => currentLevel; set => currentLevel = value; }

    /// <summary>
    /// Create user given their fields
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="features"></param>
    /// <param name="aptitude"></param>
    /// <param name="initFeatures"></param>
    /// <param name="initAptitude"></param>
	/// <param name="currentLevel"></param>
    public User(
        int id, string name, double[] features, ushort aptitude = 0,
        double[] initFeatures = null, short initAptitude = -1,
        ushort currentLevel = 0
        )
    {
        this.id = id;
        this.name = name;
        this.features = new double[GlobalVariables.nbFeatures];
        UpdateFeatures(features);
        this.aptitude = aptitude;

        this.initFeatures = new double[GlobalVariables.nbFeatures];
        if (initFeatures == null)
            UpdateFeatures(features, true);
        else
            UpdateFeatures(initFeatures, true);
        if (initAptitude < 0)
            this.initAptitude = aptitude;
        else
            this.initAptitude = (ushort)initAptitude;

        this.currentLevel = currentLevel;
    }

    /// <summary>
    /// Create user given a default value for the features
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="featuresDefaultValue"></param>
    /// <param name="aptitude"></param>
    /// <param name="initFeaturesDefaultValue"></param>
	/// <param name="initAptitude"></param>
	/// <param name="currentLevel"></param>
    public User(
        int id, string name, double featuresDefaultValue = 0.0,
        ushort aptitude = 0, double initFeaturesDefaultValue = 0.0,
        short initAptitude = -1, ushort currentLevel = 0
        )
    {
        this.id = id;
        this.name = name;
        features = new double[GlobalVariables.nbFeatures];
        UpdateFeatures(featuresDefaultValue);
        this.aptitude = aptitude;

        initFeatures = new double[GlobalVariables.nbFeatures];
        UpdateFeatures(initFeaturesDefaultValue, true);
        if (initAptitude < 0)
            this.initAptitude = aptitude;
        else
            this.initAptitude = (ushort)initAptitude;

        this.currentLevel = currentLevel;
    }

    /// <summary>
    /// Update features given an array
    /// </summary>
    /// <param name="features"></param>
	/// <param name="initFeatures"></param>
    public void UpdateFeatures(double[] features, bool initFeatures = false)
    {
        if (features.Length != GlobalVariables.nbFeatures)
            throw new ArgumentException(GlobalVariables.featuresExceptionMessage);
        for (int i = 0; i < GlobalVariables.nbFeatures; i++)
        {
            if (initFeatures)
                this.initFeatures[i] = features[i];
            else
                this.features[i] = features[i];
        }
    }

    /// <summary>
    /// Udate features given a default value
    /// </summary>
    /// <param name="value"></param>
	/// <param name="initFeatures"></param>
    public void UpdateFeatures(double value, bool initFeatures = false)
    {
        for (int i = 0; i < GlobalVariables.nbFeatures; i++)
        {
            if (initFeatures)
                this.initFeatures[i] = value;
            else
                features[i] = value;
        }
    }

    /// <summary>
    /// Dump user data to an archive
    /// </summary>
    public void DumpToArchive()
    {
        using (FileStream fs = new FileStream(GlobalVariables.archivePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
                sw.WriteLine(ToArchiveCSVLine());
    }

    /// <summary>
    /// Dump user initial data to a database used for a model training
    /// </summary>
    public void DumpInitData()
    {
        int id = 0;
        string[] lines = File.ReadAllLines(GlobalVariables.initPath);
        string line = "";
        // Check whether a csv file is already containing a data of the current user
        // We suppose that a data from the initial level could be added to the data source only once
        for (int i = 1; i < lines.Length; i++)
        {
            line = lines[i]; 
            if (line != "")
            {
                id = Int32.Parse(line.Split(GlobalVariables.csvValueSeparator[0])[GlobalVariables.csvInitIDIndex]);
                if (id == this.id)
                    throw new InvalidOperationException("The user with the same id has been already added to the data source");
            }
        }
        // Add data if it has not been done yet
        using (FileStream fs = new FileStream(GlobalVariables.initPath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
                sw.WriteLine(ToInitCSVLine());
    }

    /// <summary>
    /// Update or save user data in general users table
    /// </summary>
    public void DumpToUsers()
    {
        int id = 0;
        string[] lines = File.ReadAllLines(GlobalVariables.usersPath);
        string line = "";
        bool userFound = false;
        // Check whether a csv file is already containing a data of the current user
        for (int i = 1; i < lines.Length; i++)
        {
            line = lines[i];
            if (line != "")
            {
                id = Int32.Parse(line.Split(GlobalVariables.csvValueSeparator[0])[GlobalVariables.csvUsersIDIndex]);
                if (id == this.id)
                {
                    userFound = true;
                    lines[i] = ToUsersCSVLine();
                    break;
                }
            }
        }
        // Update user data if it is contained in a data source
        if (userFound)
            using (FileStream fs = new FileStream(GlobalVariables.usersPath, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                    foreach (string li in lines)
                        sw.WriteLine(li);
        // Unless append data of the current user to a data source
        else
            using (FileStream fs = new FileStream(GlobalVariables.usersPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                    sw.WriteLine(ToUsersCSVLine());
    }

    /// <summary>
    /// Load user data from a database
    /// </summary>
    /// <param name="index"></param>
    public void LoadFromDataBase(int index) { }

    /// <summary>
	/// Convert user data to a line corresponding to archive
	/// </summary>
	/// <returns></returns>
    public string ToArchiveCSVLine()
    {
        string line = id.ToString() + GlobalVariables.csvValueSeparator;
        line += aptitude.ToString() + GlobalVariables.csvValueSeparator;
        line += FeaturesToString(GlobalVariables.csvValueSeparator, GlobalVariables.csvValueSeparator);
        DateTime utc = DateTime.UtcNow;
        line += utc.ToString(new CultureInfo(GlobalVariables.cultureName));
        return line;
    }

    /// <summary>
	/// Convert user data to a single line of init data csv file
	/// </summary>
	/// <returns></returns>
    public string ToInitCSVLine()
    {
        if (initFeatures == null)
            throw new InvalidOperationException("No data about user's play on test level");
        string line = id.ToString() + GlobalVariables.csvValueSeparator;
        line += initAptitude.ToString() + GlobalVariables.csvValueSeparator;
        line += FeaturesToString(GlobalVariables.csvValueSeparator, initFeatures: true);
        return line;
    }

    /// <summary>
	/// Convert user data to a single line of users' csv file
	/// </summary>s
	/// <returns></returns>
    public string ToUsersCSVLine()
    {
        string line = id.ToString() + GlobalVariables.csvValueSeparator;
        line += name.ToString() + GlobalVariables.csvValueSeparator;
        line += currentLevel.ToString() + GlobalVariables.csvValueSeparator;
        line += aptitude.ToString() + GlobalVariables.csvValueSeparator;
        line += FeaturesToString(GlobalVariables.csvValueSeparator);
        return line;
    }

    /// <summary>
	/// Convert features to string
	/// </summary>
	/// <param name="separator"></param>
	/// <param name="lastSubstring"></param>
	/// <returns></returns>
    public string FeaturesToString(string separator, string lastSubstring = "", bool initFeatures = false)
    {
        string s = "";
        for (int i = 0; i < GlobalVariables.nbFeatures; i++)
            s += (initFeatures ? this.initFeatures : features)[i].ToString("0.0") +
                (i != GlobalVariables.nbFeatures - 1 ? separator : lastSubstring);
        return s;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return ToUsersCSVLine();
    }
}
