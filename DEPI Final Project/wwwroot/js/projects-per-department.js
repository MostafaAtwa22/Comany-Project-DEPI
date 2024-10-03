function GetProjects(DeptId) {
    document.getElementById("Proj").style.display = "block";
    document.getElementById("Emps").innerHTML = "";
    console.log(DeptId);

    $.ajax({
        url: "/Department/GetProjectsPerDepartment?deptId=" + DeptId,
        success: function (result) {
            document.getElementById("Proj").innerHTML =
                "<tr>" +
                "<th>Name</th>" +
                "<th>Budget</th>" +
                "<th>Location</th>" +
                "</tr>";

            console.log(result);

            for (let proj of result) {
                console.log(proj);
                document.getElementById("Proj").innerHTML +=
                    "<tr>" +
                    "<td>" + proj.name + "</td>" +
                    "<td>" + proj.budget + "</td>" +
                    "<td>" + proj.location + "</td>" +
                    "</tr>";
            }
        },
        error: function () {
            console.log("Error fetching Project.");
        }
    });
}