using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shablon
{
    public static class TemplateProcessor 
    {

        public static string ProcessTemplate(string template, object model, string startToken, string endToken)
        {
            template = ReplaceProperties(template, startToken, endToken, model);
            return template;
        }

        /// <summary>
        /// Accepts a template and an annotated view model then replaces tokens in the template with properties found in the view model
        /// </summary>
        /// <param name="template">The text template that need populating with data</param>
        /// <param name="startToken">The string that represents the start of a token</param>
        /// <param name="endToken">The string that represents the end of a token</param>
        /// <param name="model">The annotated model containing the data</param>
        /// <returns></returns>
        private static string ReplaceProperties(string template, string startToken, string endToken, object model)
        {
            foreach (PropertyInfo item in model.GetType().GetProperties())
            {
                if (item.PropertyType.GetInterface("ICollection") == null && item.PropertyType != typeof(NameValueCollection))
                {
                    TemplateAttribute attr = (TemplateAttribute)item.GetCustomAttributes(typeof(TemplateAttribute), false).FirstOrDefault();
                    if (attr != null)
                    {
                        template = template.Replace(startToken + attr.PlaceHolder + endToken, item.GetValue(model, null)?.ToString());
                    }
                }
                else // deal with collection here
                {
                    TemplateAttribute attr = (TemplateAttribute)item.GetCustomAttributes(typeof(TemplateAttribute), false).FirstOrDefault();
                    // find the starting point for the collection template and extract it, remembering where it goes
                    int startPoint = template.IndexOf(startToken + attr.CollectionStart + endToken); // Where we found the start of this token
                    if (startPoint < 0) throw new Exception("Start token not found for collection " + item.Name);
                    int endPoint = template.IndexOf(startToken + attr.CollectionEnd + endToken, startPoint);
                    if (endPoint < 0) throw new Exception("End token not found for collection " + item.Name);

                    // Get a string that represents the template for a single row, we'll use this for each item in the collection
                    string rowTemplate = template.Substring(startPoint, endPoint - startPoint + (startToken + attr.CollectionEnd + endToken).Length);

                    // remove the row template from the full template, we'll insert populated rows in place
                    template = template.Remove(startPoint, rowTemplate.Length);
                    rowTemplate = rowTemplate.Replace(startToken + attr.CollectionStart + endToken, "");
                    rowTemplate = rowTemplate.Replace(startToken + attr.CollectionEnd + endToken, "");
                    StringBuilder rowsString = new StringBuilder();

                    // we're working with a NameValueCollection
                    if (item.PropertyType == typeof(NameValueCollection))
                    {
                        NameValueCollection col = (NameValueCollection)item.GetValue(model, null);
                        foreach (string key in col)
                        {
                            string rowContent = rowTemplate;
                            rowContent = rowContent.Replace(startToken + "_Name" + endToken, key);
                            rowContent = rowContent.Replace(startToken + "_Value" + endToken, col[key].ToString());
                            rowsString.Append(rowContent);
                        }
                        template = template.Insert(startPoint, rowsString.ToString());
                    }
                    else
                    {
                        // For each item in the collection, find each property and call ReplaceProperties recursively
                        ICollection rowData = (ICollection)item.GetValue(model, null);
                        foreach (var rowItem in rowData)
                        {
                            string rowContent = rowTemplate;
                            if (rowItem.GetType().IsValueType || rowItem.GetType() == typeof(string))
                            {
                                rowContent = rowContent.Replace(startToken + attr.PlaceHolder + endToken, rowItem.ToString());
                            }
                            else
                            {
                                rowContent = ReplaceProperties(rowContent, startToken, endToken, rowItem);
                            }
                            rowsString.Append(rowContent);
                        }
                        template = template.Insert(startPoint, rowsString.ToString());
                    }
                }
            }
            return template;
        }
    }
}
