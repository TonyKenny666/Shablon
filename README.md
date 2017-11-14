# Shablon
Created by Tony Kenny. November 2017

Please make suggestions for new features and feel free to contribute!

# Ideas and Contributions
Do you need something more from your templates? Suggest and idea and we'll see if we can help!

Do you want to collaborate?  The project could benefit from unit testing and mocking, if this interests you, please contact us and get involved! 

## Future development ideas
These are just some of the ideas we'd like to add in the future

- End-of-row templates. Part of a template that is only output at the end of a 'row' in a collection, but not on the last row. 
- Start-of-row templates. Part of a template that is only output at the start of a row, but not on the first/last, as required
- Alternate row templates. For example, outputting collections with alternate row colours or designs, as specified in a template
- MVC route aware links. Add urls to files, or action links in the same way as you do in razor views, route aware so the full URL is created for you




# Introduction
Makes the task of creating and using templates much easier by using simple tokens in a file and annotations on a model.

No need to create a separate method for each template type you use. Just create your template and model, then call invoke the processor to work its magic. 

Supports complex data types & collections.

# Usage
See example project.

Create a template with some tokens (place holders)
Create a model with annotated public properties
Invoke the processor, sending the template, model and the strings you use as the token start and end.

template = Shablon.TemplateProcessor.ProcessTemplate(template, orderModel, "[#", "#]");

## ValueTypes
Add annotation to a value type and strings to define the placeholder for auto replacement

## NameValueCollections
These are a special case and easy to use. Both the name and the value can be output in the template with the special reserved tokens "_Name" and "_Value"

# Notes
Replacements are global, so be careful not to use the same place holder in a model that contains complex types. For example, don't use "Name" for a customer on an invoice template then also use "Name" on a product. They will all get replaced with the same data!
