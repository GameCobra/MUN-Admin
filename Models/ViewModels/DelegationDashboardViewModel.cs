namespace MUNAdmin.Models.ViewModels
{
    public class DelegationDashboardViewModel
    {
        public required string MUNTitle { get; set; }
        public required DelegationInstance ActiveDelegation { get; set; }
        public required CouncilInformation ParticipatingCouncil { get; set; }
        public required List<List<Ammendment>> AmmendmentsOnResolutions { get; set; }

        //Shorthands
        public List<string> CouncilNames => ActiveDelegation.CouncilList.Select(x => x.Council.CouncilName).ToList();
        public string DelegationCountry => ActiveDelegation.DelegationCountry;
        public string GradiantPrimaryColor => ParticipatingCouncil.GradientPrimaryColor;
        public string GradiantSecondaryColor => ParticipatingCouncil.GradientSecondaryColor;
        public bool SessionActive => ParticipatingCouncil.IsInSession;
        public string CurrentCouncilName => ParticipatingCouncil.CouncilName;
    }
}
