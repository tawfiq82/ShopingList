shopingListApp.controller('userController', ['$scope', '$http', function ($scope, $http) {
    $scope.userList = [];
    $scope.userTypes = [{ Id: 1, Name: "Admin" }, { Id: 2, Name: "Normal" }];
    $scope.isAdding = false;
    $scope.isDeleting = false;
    $scope.isDuplicateUser = false;
    $scope.isUpdating = false;

    $http.get('/Users/GetUsers')
        .success(function (data) {
            $scope.userList = data;
            $.each($scope.userList, function (indx, val) {
                setUserTypeName(val);
                val.IsUpdating = false;
                val.HideControls = false;
            });
        }).error(function (response) {
            alertBox.showErrorMessage(response.message);
            console.log(response.message);
        });

    $scope.addUser = function (form) {
        $scope.isAdding = true;
        if (form && form.$invalid)
            return;

        if (isDuplicateUser($scope.user)) {
            $scope.isDuplicateUser = true;
            return;
        }

        alertBox.hide();
        $http.post('/Users/AddUserAsync', $scope.user)
            .success(function (data) {
                data = data.replace('"', '').replace('"', '');
                onSuccessAddUser(data);
            }).error(function (response) {
                alertBox.showErrorMessage(response.message);
                console.log(response.message);
            }).finally(function () {
                $scope.isAdding = false;
            });
    }

    $scope.deleteUser = function (userId) {
        alertBox.hide();
        $scope.isDeleting = true;
        $http.post('/Users/DeleteUserAsync', { "userId": userId })
                    .success(function (data) {
                        onSuccessDeleteUser(userId);
                    }).error(function (response) {
                        alertBox.showErrorMessage(response.message);
                        console.log(response.message);
                    }).finally(function () {
                        $scope.isDeleting = false;
                    });
    }

    $scope.checkDuplicateUser = function (user) {
        $scope.isDuplicateUser = isDuplicateUser(user);
    }

    function onSuccessAddUser(userId) {
        $scope.user.UserId = userId;
        var newUser = $.extend(true, {}, $scope.user);
        setUserTypeName(newUser);
        $scope.userList.splice(0, 0, newUser);
        $scope.user.Name = '';
        $scope.user.Type = '';
        $scope.user.UserId = '';
        alertBox.showSuccessMessage("Successfully added the user.");
    }

    function onSuccessDeleteUser(userId) {
        var index = -1;
        $.each($scope.userList, function (indx, val) {
            if (val.UserId === userId) {
                index = indx;
            }
        });

        if (index >= 0)
            $scope.userList.splice(index, 1);
        alertBox.showSuccessMessage("Successfully deleted the user.");
    }

    function setUserTypeName(user) {
        var userType = $.grep($scope.userTypes, function (val) {
            return val.Id === parseInt(user.Type);
        });
        if (userType != null && userType.length > 0)
            user.TypeName = userType[0].Name;
    }

    function isDuplicateUser(user) {
        if (user.Name === '') return false;
        var duplicateUser = $.grep($scope.userList, function (val) {
            return val.Name.toUpperCase() === user.Name.toUpperCase()
                && (user.UserId == undefined || val.UserId !== user.UserId);
        });

        if (duplicateUser != null && duplicateUser.length > 0)
            return true;

        return false;
    }

    $scope.editUser = function (user) {
        $.each($scope.userList, function (indx, val) {
            if (val.UserId === user.UserId) {
                val.IsUpdating = true;
            } else {
                val.HideControls = true;
            }
        });
        $scope.isUpdating = true;
    }

    $scope.updateUser = function (form, user) {

        var invalid = false;

        if (user.Name == undefined || user.Name === '') {
            form.updateName.$error.required = true;
            invalid = true;
        } else {
            form.updateName.$error.required = false;
        }

        if (user.Type == undefined) {
            form.updateType.$error.required = true;
            invalid = true;
        } else {
            form.updateType.$error.required = false;
        }

        if (invalid || $scope.isDuplicateUser)
            return;

        alertBox.hide();
        $http.post('/Users/UpdateUserAsync', user)
            .success(function (data) {
                setUserTypeName(user);
                alertBox.showSuccessMessage("Successfully updated the user.");
            }).error(function (response) {
                alertBox.showErrorMessage(response.message);
                console.log(response.message);
            }).finally(function () {
                $.each($scope.userList, function (indx, val) {
                    if (val.UserId === user.UserId) {
                        val.IsUpdating = false;
                    } else {
                        val.HideControls = false;
                    }
                });
                $scope.isUpdating = false;
            });
    }

    $scope.cancelUpdate = function (user) {
        $.each($scope.userList, function (indx, val) {
            if (val.UserId === user.UserId) {
                val.IsUpdating = false;
            } else {
                val.HideControls = false;
            }
        });
        $scope.isUpdating = false;
    }
}]);