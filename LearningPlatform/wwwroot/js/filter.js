function Filter(num) {
    var input, filt, list, tr, td, txtValue;

    input = document.getElementById("myInput");
    filt = input.value.toUpperCase();
    list = document.getElementById("list");
    tr = list.getElementsByTagName("tr");

    for (let i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td");

        if (!filt) {
            tr[i].style.display = "";
            continue;
        }

        var isMatch = false;
        for (let j = 0; j < num; j++) {
            txtValue = td[j].textContent || td[j].innerText;
            if (txtValue.toUpperCase().indexOf(filt) > -1) {
                isMatch = true;
                break;
            }
        }
        tr[i].style.display = isMatch ? "" : "none";
    }
}