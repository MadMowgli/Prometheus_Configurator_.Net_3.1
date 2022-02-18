# Prometheus Configurator (.NET Core 3.1)
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

