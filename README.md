# Prometheus Configurator (.NET Core 3.1)
![Prometheus_Configurator.png](Resources/Screenshots/UI_Add.png)
> The Prometheus Configurator is a Blazor Plugin you can use to configure the Prometheus YAML config file with ease.
> The plugin consists of some logic mostly embedded in a razor component, which offers an UI to create simple CRUD operations on the Prometheus config file.


## How To Use
> Long story short:
> 1. Upload your existing prometheus.yml configuration file using the **Upload Configuration Tab**
> 2. Make changes as you wish, using the UI of the configurator (each change is saved on button click)
> 3. When you're done, click the blue **Download Files** link to obtain a .zip which contains all the files you need

The Prometheus Configurator lets you manipulate a Prometheus configuration in a graphical way.
To do so, you first need to upload an already existing configuration (your "classic" prometheus.yml file) by switching to the **Upload Configuration**-Tab (click on the left arrow) and
uploading your existing configuration using the **Browse** and then the yellow **Upload** button. Once you've uploaded your config file, the configurator
creates an in-memory model, which you can manipulate on. You can add, update or remove scraping targets using the UI, feeding it the required information.
**Every action you take (Adding, Updating or Removing a scraping target) is saved immediately as you click the according button.** However, some people may want to do so manually, that's
why there is a save-button.

## How to implement
A few things to notice when implementing this plugin to your already existing Blazor solution:
- **Changes on the _Host.cshtml file**
  - I've added 3 script tags at the end of the _Host.cshtml file:
    - `<script src="js/bootstrap.bundle.js"></script>`
    - `<script src="_content/BlazorInputFile/inputfile.js"></script>`
    - `<script src="/js/BlazorDownloadFile.js"></script>`
- Make sure to adjust namespaces.

## Dependencies
- [BlazorInputFile v.0.2.0](https://www.nuget.org/packages/BlazorInputFile)
- [Newtonsoft.Json v.13.0.1](https://www.nuget.org/packages/Newtonsoft.Json)
- [Tewr.Blazor.FileReader v.3.3.1.21360](https://www.nuget.org/packages/Tewr.Blazor.FileReader)
- [YamlDotNet v.11.2.1](https://www.nuget.org/packages/YamlDotNet)
- [Bootstrap 5](https://getbootstrap.com/docs/5.0/getting-started/introduction/) using [self compiled sass files](https://www.youtube.com/watch?v=9b4hYVNCFK4)



