namespace IdentityManager.Entities
{
    using System.Collections.Generic;

    public class IdentityInfor
    {
        public IdentityInfor()
        {
            this._errors = new List<string>();
        }

        private List<string> _errors;

        public List<string> Errors => _errors == null ? _errors = new List<string>() : _errors;

        public bool IsError => Errors.Count > 0;

        public object Data { get; set; }
    }
}
