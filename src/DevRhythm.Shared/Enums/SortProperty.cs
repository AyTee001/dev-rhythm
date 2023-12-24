using System.ComponentModel.DataAnnotations;

namespace DevRhythm.Shared.Enums
{
    public enum SortProperty
    {
        [Display(Name = "No_sort")]
        None,
        [Display(Name = "Creation_date")]
        CreatedAt,
        [Display(Name = "Popularity")]
        VoteResult
    }
}
