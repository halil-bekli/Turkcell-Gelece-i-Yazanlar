﻿namespace MyAspNetCoreApp2.Web.Helpers {
    public class Helper : IHelper {

        private bool _isConfiguration;

        public Helper(bool isConfiguration) {
            _isConfiguration = isConfiguration;
        }

        public string Upper(string text) {
            return text.ToUpper();
        }
    }
}
