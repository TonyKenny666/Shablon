using System;
using System.Collections.Generic;

namespace Shablon
{
    public class TemplateAttribute : System.Attribute
    {
        /// <summary>
        /// The case-sensitive text to match in the placeholder
        /// </summary>
        public string PlaceHolder { get; set; }

        /// <summary>
        /// The case-sensitive text to match for the start of a collection of items (rows)
        /// </summary>
        public string CollectionStart { get; set; }

        /// <summary>
        /// The case-sensitive text to match to find the end of the collection of itmems (rows) 
        /// </summary>
        public string CollectionEnd { get; set; }

    }
}
