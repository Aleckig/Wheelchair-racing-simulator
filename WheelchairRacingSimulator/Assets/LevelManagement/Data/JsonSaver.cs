using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace LevelManagement.Data
{
   // The class responsible for saving and loading game data in JSON format
    public class JsonSaver
    {
        // The filename for saving and loading data
        private static readonly string filename = "saveData1.sav";

        // Get the full path of the save file
        public static string GetSaveFilename()
        {
            return Application.persistentDataPath + "/" + filename;
        }

        // Save method that takes a SaveData object as input
        public void Save(SaveData data)
        {
            // Convert the SaveData object to a JSON string
            string json = JsonUtility.ToJson(data);

            // Get the full path of the save file
            string saveFilename = GetSaveFilename();

            // Open a file stream for writing
            FileStream fileStream = new FileStream(saveFilename, FileMode.Create);

            // Use a StreamWriter to write the JSON string to the file
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }

            // The file stream will be closed automatically when leaving this block
        }

        // Load method that takes a SaveData object as input and returns a boolean indicating success
        public bool Load(SaveData data)
        {
            // Get the full path of the save file
            string loadFilename = GetSaveFilename();

            // Check if the file exists
            if (File.Exists(loadFilename))
            {
                // Open a file stream for reading
                using (StreamReader reader = new StreamReader(loadFilename))
                {
                    // Read the entire content of the file as a JSON string
                    string json = reader.ReadToEnd();

                    // Deserialize the JSON string into the provided SaveData object
                    JsonUtility.FromJsonOverwrite(json, data);
                }

                // Return true indicating successful loading
                return true;
            }

            // Return false indicating that the file does not exist
            return false;
        }

        // Delete method to delete the save file
        public void Delete()
        {
            // Delete the save file
            File.Delete(GetSaveFilename());
        }
    }

}
