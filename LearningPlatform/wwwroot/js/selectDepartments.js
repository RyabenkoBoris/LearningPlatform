const departments = document.getElementById("department");
function selectDepartments(model) {
    if (faculties.selectedIndex === 0) {
        for (let i = 0; i < departments.length; i++) {
            departments.options[i].hidden = false;
        }
        return;
    }
    for (let i = 0; i < departments.length; i++) {
        departments.options[i].hidden = true;
    }
    for (let department of model) {
        if (department.FacultyId == faculties.value) {
            for (let i = 0; i < departments.length; i++) {
                let option = departments.options[i];
                if (option.value == department.Id) {
                    option.hidden = false;
                    departments.value = option.value;
                }
            }
        }
    }
}