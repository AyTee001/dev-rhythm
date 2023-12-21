using System.ComponentModel.DataAnnotations;

namespace DevRhythm.Shared.Enums
{
    public enum SortProperty
    {
        [Display(Name = "No sort")]
        None,
        [Display(Name = "Creation date")]
        CreatedAt,
        [Display(Name = "Popularity")]
        VoteResult
    }
}
