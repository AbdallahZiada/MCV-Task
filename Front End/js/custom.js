var departmentValidator = new FormValidator({"events" : ['blur', 'paste', 'change']}, document.getElementById("deptForm"));
var employeeValidator = new FormValidator({"events" : ['blur', 'paste', 'change']}, document.getElementById("empForm"));

$(function() {
    $("#birthdatepicker").datepicker({ dateFormat: "yy-mm-dd" });
    $("#employeehiringDate").datepicker({ dateFormat: "yy-mm-dd" });

    getDepartments();
    getEmployees();


    $('#deptModalCenter').on('hidden.bs.modal', function (e) {
        resetDepartmentForm();
    });

    $('#empModalCenter').on('hidden.bs.modal', function (e) {
        resetEmployeeForm();
    });

    $('#empForm').submit(function(e) {
        var validatorResult = employeeValidator.checkAll(this);
        if(!!validatorResult.valid){
            if($("#employeeId").val() > 0){
                updateEmployee();
            }
            else{
                createEmployee();
            }
        }
        return false;
    });

    $('#deptForm').submit(function(e) {
        var validatorResult = departmentValidator.checkAll(this);
        if(!!validatorResult.valid){
            if($("#departmentId").val() > 0){
                updateDepartment();
            }
            else{
                createDepartment();
            }
        }
        return false;
    });
});

// Department
function getDepartments(){
    $("#departments").html("");
    $("#departmentdd").html("");
    $.ajax('https://localhost:7019/api/Department/GetAll', 
    {
        dataType: 'json',
        timeout: 5000,
        success: function (data) {
            $("#departmentdd").append(`<option value="">-- please select --</option>`)
            let i = 1;
            for(let dept of data.dataList)
            {
                $("#departments").append(`<tr>
                                            <th scope="row">`+(i++)+`</th>
                                            <td>`+dept.name+`</td>
                                            <td>
                                                <button class="btn btn-warning" onclick="getDepartmentDetails(`+dept.id+`)">Edit</button>
                                            </td>
                                            <td>
                                                <button class="btn btn-danger" href="#DeleteDeptModal" data-toggle="modal" onclick="confirmdeleteDepartment(`+dept.id+`)">Delete</button>
                                            </td>
                                        </tr>`);

                $("#departmentdd").append(`<option value="`+dept.id+`">`+dept.name+`</option>`)

            }
        },
        error: function (data) {
            alert("faild getting department list")
        }
    });
}

function getDepartmentDetails(id) {
    $('#deptModalCenter').modal('show');
    $.ajax('https://localhost:7019/api/Department/Get?id='+id, 
    {
        dataType: 'json',
        timeout: 5000,
        success: function (data) {
                $("#departmentId").val(data.responseData.id)
                $("#departmentname").val(data.responseData.name)
        },
        error: function (data) {
            alert("faild getting department to edit")
        }
    });
}

function createDepartment(){
    $.ajax({
        type: "POST",
        url: "https://localhost:7019/api/Department/Add",
        contentType: 'application/json',
        timeout: 5000,
        data: JSON.stringify({
            name : $("#departmentname").val()
        })
    }).done(function (data) {
        getDepartments();
        $('#deptModalCenter').modal('toggle');
        new PNotify({
            title: 'Successfully added new department',
            text: "new department is added",
            type: 'success',
            styling: 'bootstrap3'
        })
    }).fail(function (data) {
        alert("faild add department");
        new PNotify({
            title: 'Failed to add department',
            text: data.responseJSON.errors[Object.keys(data.responseJSON.errors)[0]] ?? "Somthing wrong",
            type: 'fail',
            styling: 'bootstrap3'
        })
    })
}

function updateDepartment(){
    $.ajax({
        type: "PUT",
        url: "https://localhost:7019/api/Department/Update",
        contentType: 'application/json',
        timeout: 5000,
        data: JSON.stringify({
            id : $("#departmentId").val(),
            name : $("#departmentname").val()
        })
    }).done(function (data) {
        getDepartments();
        $('#deptModalCenter').modal('toggle');
        new PNotify({
            title: 'Successfully added new department',
            text: "department has been updated",
            type: 'success',
            styling: 'bootstrap3'
        })
    }).fail(function (data) {
        new PNotify({
            title: 'Failed to update department',
            text: data.responseJSON.errors[Object.keys(data.responseJSON.errors)[0]] ?? "Somthing wrong",
            type: 'fail',
            styling: 'bootstrap3'
        })
    })
}

function confirmdeleteDepartment(id){
    $("#deletedeptId").val(id);
}

function deleteDept(){
    let deptId = $("#deletedeptId").val();
    $.ajax({
        type: "DELETE",
        url: "https://localhost:7019/api/Department/Delete",
        contentType: 'application/json',
        timeout: 5000,
        data: JSON.stringify({
            id : deptId,
        })
    }).done(function (data) {
        getDepartments();
        $('#DeleteDeptModal').modal('toggle');
        new PNotify({
            title: 'Department deleted successfully',
            text: "Department has been deleted",
            type: 'success',
            styling: 'bootstrap3'
        })
    }).fail(function (data) {
        new PNotify({
            title: 'Failed to delete department',
            text: data.responseJSON.errors[Object.keys(data.responseJSON.errors)[0]] ?? "Somthing wrong",
            type: 'fail',
            styling: 'bootstrap3'
        })
    })
}

function resetDepartmentForm(){
    $("#deptForm").trigger('reset');
    $("#departmentId").val("0");
    departmentValidator.reset();
}

// Employee
function getEmployees(){
    $("#employees").html("");
    $.ajax('https://localhost:7019/api/Employee/GetAll', 
    {
        dataType: 'json',
        timeout: 5000,
        success: function (data) {
            let i = 1;
            for(let emp of data.dataList)
            {
                $("#employees").append(`<tr>
                                            <th scope="row">`+(i++)+`</th>
                                            <td>`+emp.employeeId+`</td>
                                            <td>`+emp.name+`</td>
                                            <td>`+emp.phoneNumber+`</td>
                                            <td>`+emp.birthDate+`</td>
                                            <td>`+emp.jobTitle+`</td>
                                            <td>`+emp.hiringDate+`</td>
                                            <td>`+emp.department.name+`</td>
                                            <td>
                                                <button class="btn btn-warning" onclick="getEmployeeDetails(`+emp.id+`)">Edit</button>
                                            </td>
                                            <td>
                                                <button class="btn btn-danger" href="#DeleteEmpModal" data-toggle="modal" onclick="confirmdeleteEmployee(`+emp.id+`)">Delete</button>
                                            </td>
                                        </tr>`)
            }
        },
        error: function (data) {
            alert("faild getting employee list")
        }
    });
}

function getEmployeeDetails(id){
    $('#empModalCenter').modal('show');
    $.ajax('https://localhost:7019/api/Employee/Get?id='+id, 
    {
        dataType: 'json',
        timeout: 5000,
        success: function (data) {
                $("#employeeId").val(data.responseData.id)
                $("#empId").val(data.responseData.employeeId)
                $("#employeename").val(data.responseData.name)
                $("#employeephoneNumber").val(data.responseData.phoneNumber)
                $("#birthdatepicker").val(data.responseData.birthDate)
                $("#employeejobTitle").val(data.responseData.jobTitle)
                $("#employeehiringDate").val(data.responseData.hiringDate);
                $("#departmentdd > option[value='"+data.responseData.department.id+"']").attr("selected","selected");
        },
        error: function (data) {
            alert("faild getting department to edit")
        }
    });
}

function createEmployee(){
    $.ajax({
        type: "POST",
        url: "https://localhost:7019/api/Employee/Add",
        contentType: 'application/json',
        timeout: 5000,
        data: JSON.stringify({
            employeeId : $("#empId").val(),
            name : $("#employeename").val(),
            phoneNumber : $("#employeephoneNumber").val(),
            birthDate : new Date($("#birthdatepicker").val()),
            title : $("#employeejobTitle").val(),
            hiringDate :  new Date($("#employeehiringDate").val()),
            departmentId : $("#departmentdd").val(),
        })
    }).done(function (data) {
        getEmployees();
        $('#empModalCenter').modal('toggle');
        new PNotify({
            title: 'Successfully added new employee',
            text: "new employee is added",
            type: 'success',
            styling: 'bootstrap3'
        })
    }).fail(function (data) {
        new PNotify({
            title: 'Failed to add employee',
            text: data.responseJSON.errors[Object.keys(data.responseJSON.errors)[0]] ?? "Somthing wrong",
            type: 'fail',
            styling: 'bootstrap3'
        })
    })
}

function updateEmployee(){
    $.ajax({
        type: "PUT",
        url: "https://localhost:7019/api/Employee/Update",
        contentType: 'application/json',
        timeout: 5000,
        data: JSON.stringify({
            id : $("#employeeId").val(),
            employeeId : $("#empId").val(),
            name : $("#employeename").val(),
            phoneNumber : $("#employeephoneNumber").val(),
            birthDate :  new Date($("#birthdatepicker").val()),
            title : $("#employeejobTitle").val(),
            hiringDate : new Date($("#employeehiringDate").val()),
            departmentId : $("#departmentdd").val(),
        })
    }).done(function (data) {
        getEmployees();
        $('#empModalCenter').modal('toggle');
        new PNotify({
            title: 'Successfully updated employee',
            text: "employee has been updated",
            type: 'success',
            styling: 'bootstrap3'
        })
    }).fail(function (data) {
        new PNotify({
            title: 'Failed to update employee',
            text: data.responseJSON.errors[Object.keys(data.responseJSON.errors)[0]] ?? "Somthing wrong",
            type: 'fail',
            styling: 'bootstrap3'
        })
    })
}

function confirmdeleteEmployee(id){
    $("#deleteempId").val(id);
}

function deleteEmp(){
    let empId = $("#deleteempId").val();
    $.ajax({
        type: "DELETE",
        url: "https://localhost:7019/api/Employee/Delete",
        contentType: 'application/json',
        timeout: 5000,
        data: JSON.stringify({
            id : empId,
        })
    }).done(function (data) {
        getEmployees();
        $('#DeleteEmpModal').modal('toggle');
        new PNotify({
            title: 'Employee deleted successfully',
            text: "Employee has been deleted",
            type: 'success',
            styling: 'bootstrap3'
        })
    }).fail(function (data) {
        new PNotify({
            title: 'Failed to delete employee',
            text: data.responseJSON.errors[Object.keys(data.responseJSON.errors)[0]] ?? "Somthing wrong",
            type: 'fail',
            styling: 'bootstrap3'
        })
    })
}

function resetEmployeeForm(){
    $("#empForm").trigger('reset');
    $("#employeeId").val("0");
    $("#departmentdd > option").removeAttr('selected');
    employeeValidator.reset();
}

