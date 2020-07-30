using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeldCalculator.Butt
{
    class DataError
    {
        private List<string> errors;
        public List<string> Errors { get => errors; set => errors = value; }

        public DataError()
        {
            Errors = new List<string>();  
        }

        public DataError(string error)
        {
            Errors = new List<string>();
            Errors.Add(error);
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public int Count()
        {
            return Errors.Count;
        }

        public void ShowErrors()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(string error in Errors)
            {
                stringBuilder.Append("- " + error + "\n");
            }

            if(stringBuilder.Length>0) MessageBox.Show(stringBuilder.ToString(), "Errors");
        }

        public void Clear()
        {
            Errors.Clear();
        }
    }
}
