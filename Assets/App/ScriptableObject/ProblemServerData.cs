using UnityEngine;

[CreateAssetMenu(fileName = "ProblemServerData", menuName = "Scriptable Objects/ProblemServerData")]
public class ProblemServerData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        public string quetionProblem;
        public string questionText;
        public string[] options = new string[3]; // 4 pilihan jawaban
        public int correctOptionIndex; // Index jawaban benar (0-3)
        public Sprite questionImage; // Optional: gambar pendukung
    }

    public Question[] questions;

    // Validasi data di Editor
    void OnValidate()
    {
        if (questions != null)
        {
            foreach (var q in questions)
            {
                if (q.options.Length != 3)
                {
                    Debug.LogWarning($"Question '{q.questionText}' harus memiliki 3 opsi jawaban!");
                    System.Array.Resize(ref q.options, 3);
                }

                if (q.correctOptionIndex < 0 || q.correctOptionIndex >= q.options.Length)
                {
                    Debug.LogError($"Index jawaban benar tidak valid untuk pertanyaan: {q.questionText}");
                    q.correctOptionIndex = 0;
                }
            }
        }
    }
}
