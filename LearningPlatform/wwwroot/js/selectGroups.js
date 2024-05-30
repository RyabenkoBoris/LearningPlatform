const groups = document.getElementById("group");
function selectGroups(groupModel, departmentModel) {
    if (faculties.selectedIndex === 0) {
        for (let i = 0; i < groups.length; i++) {
            groups.options[i].hidden = false;
        }
        return;
    }
    for (let i = 0; i < groups.length; i++) {
        groups.options[i].hidden = true;
    }
    for (let department of departmentModel) {
        if (department.FacultyId == faculties.value) {
            let passedGroups = groupModel.filter(g => g.DepartmentId == department.Id);
            for(let item of passedGroups)
            {
                for (let i = 0; i < groups.length; i++) {
                    let option = groups.options[i];
                    if (option.value == item.Id) {
                        option.hidden = false;
                        groups.value = option.value;
                    }
                }
            }
        }
    }
}