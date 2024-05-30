using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.ViewModels
{
    public class JournalEditViewModel
    {
        [HiddenInput]
        public int GroupId { get; set; }
        [HiddenInput]
        public int JournalId { get; set; }
        [HiddenInput]
        public string Name { get; set; }
        [HiddenInput]
        public string GroupName { get; set; }
        public byte Semester { get; set; }

    }
}
