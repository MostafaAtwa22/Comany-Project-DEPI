function GetEmployees(DeptId) {
    document.getElementById("Emps").style.display = "block";
    document.getElementById("Proj").innerHTML = "";
    console.log(DeptId);

    $.ajax({
        url: "/Department/GetEmployeesPerDepartment?deptId=" + DeptId,
        success: function (result) {
            document.getElementById("Emps").innerHTML =
                "<tr>" +
                "<th>Name</th>" +
                "<th>Age</th>" +
                "<th>Salary</th>" +
                "</tr>";

            console.log(result);

            for (let st of result) {
                console.log(st);
                document.getElementById("Emps").innerHTML +=
                    "<tr>" +
                    "<td>" + st.name + "</td>" +
                    "<td>" + st.age + "</td>" +
                    "<td>" + st.salary + "</td>" +
                    "</tr>";
            }
        },
        error: function () {
            console.log("Error fetching employees.");
        }
    });
}