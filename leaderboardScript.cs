using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Linq;

public class leaderboardScript : MonoBehaviour
{

private List<string[]> rowData = new List<string[]>();
private string[] names =  { "Joshua", "Alfonso", "Alex", "Sam", "Adam", "Rox", "Marta", "Krsna"};
private int[] scores = {25, 20, 21, 30, 44, 2, 50, 45};
// private string[] names = {"Josh", "Adam", "Raul"};
// private int[] scores = {25, 30, 27};
private char lineSeperator = '\n'; 
private char fieldSeperator = ','; 
Dictionary<string, int> dictionary = new Dictionary<string, int> {};
List<KeyValuePair< string, int>> leaderboardList = new List< KeyValuePair< string, int> > {};

//Outlets:
public Text nameTextOne;
public Text nameTextTwo;
public Text nameTextThree;
public Text nameTextFour;
public Text nameTextFive;

public Text scoreTextOne;
public Text scoreTextTwo;
public Text scoreTextThree;
public Text scoreTextFour;
public Text scoreTextFive;

public Text rankTextOne;
public Text rankTextTwo;
public Text rankTextThree;
public Text rankTextFour;
public Text rankTextFive;

public Button searchButton;
public InputField nameInput;
public Text searchResult;




    // Start is called before the first frame update
    void Start()
    {
        searchButton.onClick.AddListener(Search);
        //Save();
        Load();

    }

    void Search() {
        //Get name from InputField
        var name = nameInput.text;
        bool found = false;

        //Search leaderboardList for name
        var count = leaderboardList.Count;
        for (int i = 0; i < count; i++ ) {
            if (leaderboardList[i].Key == name) {
                string result = $" {name} you are ranked number {i+1} out of {count}";
                searchResult.text = result;
                found = true;
                break;
            }
        }
        if (!found) {
            searchResult.text = "No record of this user found!";

        }


        // Debug.Log(name);
    }

    public void Save(string name, int score){


        //string[] rowDataTemp = new string[2];
        //rowDataTemp[0] = "Name";
        //rowDataTemp[1] = "Score";

        //rowData.Add(rowDataTemp);

        //for(int i = 0; i < names.length; i++){
        //    rowdatatemp = new string[2];
        //    rowdatatemp[0] = names[i]; 
        //    rowdatatemp[1] = scores[i].tostring(); // id
        //    rowdata.add(rowdatatemp);

        //}

        string[] rowDataTemp = new string[2];
        rowDataTemp[0] = name;
        rowDataTemp[1] = score.ToString();
        rowData.Add(rowDataTemp);
        string[][] output = new string[rowData.Count][];

        for(int i = 0; i < output.Length; i++){
            output[i] = rowData[i];

        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();
        
        for (int index = 0; index < length; index++) {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        string fileDir = Application.dataPath +"/Leaderboard"; //Create directory
        string filePath = Application.dataPath +"/Leaderboard/"+"leaderboard.csv";

        System.IO.Directory.CreateDirectory(fileDir);
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }


    void Load() {
        string fileDir = Application.dataPath +"/Leaderboard";
        string filePath = Application.dataPath +"/Leaderboard/"+"leaderboard.csv";

        bool exists = Directory.Exists(fileDir);
        if (exists) {
            List<string> nameList = new List<string>();
            List<int> scoreList = new List<int>();

            string[] fetchNames = new string[] {};
            int[] fetchScores = new int[] {};

            using(var reader = new StreamReader(filePath)) {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != "") {
                        var values = line.Split(',');
                        if (values[0] != "Name") {
                            nameList.Add(values[0]);
                            fetchNames = nameList.ToArray();
                        }
                        if (values[1] != "Score") {
                            scoreList.Add(int.Parse(values[1]));
                            fetchScores = scoreList.ToArray();
                        }

                    }
                }
            }
            int size = fetchNames.Length;

            for (int i = 0; i < size; i++) {
                dictionary.Add(fetchNames[i], fetchScores[i]);
            }

            var leaderboard = from pair in dictionary
                orderby pair.Value descending
                select pair;

            foreach (KeyValuePair<string, int> pair in leaderboard) {
                leaderboardList.Add(pair);
            }


            if (leaderboardList.Count >= 5) {
                // Only use 5 highest scores
                updateLeaderboard(5);

            }
            else {
                // Hide outlets for score fields that aren't in use
                hideTextFields(leaderboardList.Count);
                updateLeaderboard(leaderboardList.Count);
            }
        
        }
        else {
            Debug.Log("File path does not exist");
        }

    }

    void hideTextFields(int size) {
        switch(size) {
            case 1:
                rankTextTwo.text = "";
                scoreTextTwo.text = "";
                nameTextTwo.text = "";

                rankTextThree.text = "";
                scoreTextThree.text = "";
                nameTextThree.text = "";

                rankTextFour.text = "";
                scoreTextFour.text = "";
                nameTextFour.text = "";

                rankTextFive.text = "";
                scoreTextFive.text = "";
                nameTextFive.text = "";
                break;

            case 2:
                rankTextThree.text = "";
                scoreTextThree.text = "";
                nameTextThree.text = "";

                rankTextFour.text = "";
                scoreTextFour.text = "";
                nameTextFour.text = "";

                rankTextFive.text = "";
                scoreTextFive.text = "";
                nameTextFive.text = "";
                break;

            case 3:
                rankTextFour.text = "";
                scoreTextFour.text = "";
                nameTextFour.text = "";

                rankTextFive.text = "";
                scoreTextFive.text = "";
                nameTextFive.text = "";
                break;

            case 4:
                rankTextFive.text = "";
                scoreTextFive.text = "";
                nameTextFive.text = "";
                break;

        }
    }

    void updateLeaderboard(int size) {
        switch(size) {
            case 1:
                nameTextOne.text = leaderboardList[0].Key;
                scoreTextOne.text = leaderboardList[0].Value.ToString();
                break;
            case 2:
                nameTextOne.text = leaderboardList[0].Key;
                scoreTextOne.text = leaderboardList[0].Value.ToString();
                nameTextTwo.text = leaderboardList[1].Key;
                scoreTextTwo.text = leaderboardList[1].Value.ToString();
                break;

            case 3:
                nameTextOne.text = leaderboardList[0].Key;
                scoreTextOne.text = leaderboardList[0].Value.ToString();
                nameTextTwo.text = leaderboardList[1].Key;
                scoreTextTwo.text = leaderboardList[1].Value.ToString();
                nameTextThree.text = leaderboardList[2].Key;
                scoreTextThree.text = leaderboardList[2].Value.ToString();
                break;
            case 4:
                nameTextOne.text = leaderboardList[0].Key;
                scoreTextOne.text = leaderboardList[0].Value.ToString();

                nameTextTwo.text = leaderboardList[1].Key;
                scoreTextTwo.text = leaderboardList[1].Value.ToString();

                nameTextThree.text = leaderboardList[2].Key;
                scoreTextThree.text = leaderboardList[2].Value.ToString();

                nameTextFour.text = leaderboardList[3].Key;
                scoreTextFour.text = leaderboardList[3].Value.ToString();
                break;

            case 5:
                nameTextOne.text = leaderboardList[0].Key;
                scoreTextOne.text = leaderboardList[0].Value.ToString();

                nameTextTwo.text = leaderboardList[1].Key;
                scoreTextTwo.text = leaderboardList[1].Value.ToString();

                nameTextThree.text = leaderboardList[2].Key;
                scoreTextThree.text = leaderboardList[2].Value.ToString();

                nameTextFour.text = leaderboardList[3].Key;
                scoreTextFour.text = leaderboardList[3].Value.ToString();

                nameTextFive.text = leaderboardList[4].Key;
                scoreTextFive.text = leaderboardList[4].Value.ToString();
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
