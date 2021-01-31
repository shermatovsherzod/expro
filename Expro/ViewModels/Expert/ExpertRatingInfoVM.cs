using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ExpertRatingInfoVM
    {                
        public string OverallRating { get; set; }      
        public int FiveStarsSum { get; set; }        
        public int FourStarsSum { get; set; }     
        public int ThreeStarsSum { get; set; }   
        public int TwoStarsSum { get; set; } 
        public int OneStarsSum { get; set; }
    }
}
