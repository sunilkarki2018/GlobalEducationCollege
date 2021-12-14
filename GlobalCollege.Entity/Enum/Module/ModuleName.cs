using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    public enum ModuleName
    {

        [Display(Name = "News Feed", Description = "fa-feed")]
        NewsFeed,
        [Display(Name = "Setting", Description = "fa-gears")]
        Setting,
        [Display(Name = "Menu Management", Description = " fa-list")]
        MenuManagement,
        [Display(Name = "Configuration", Description = "fa-plug")]
        Configuration,
        [Display(Name = "User Management", Description = " fa-user-plus")]
        Administrator,
        [Display(Name = "Content Management", Description = " fa-search")]
        ContentManagement,
        [Display(Name = "Page Management", Description = " fa-chrome")]
        PageManagement,
        [Display(Name = "Workflow Management", Description = "fa-users")]
        WorkflowManagement,
        [Display(Name = "Document Management", Description = "fa-folder")]
        DocumentManagement
    }
}
