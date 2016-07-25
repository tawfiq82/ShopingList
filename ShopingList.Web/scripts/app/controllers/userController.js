shopingListApp.controller('userController', ['$scope', '$http', function ($scope, $http) {
    $scope.userList = [];
    $scope.userTypes = [{ Id: 1, Name: "Admin" }, { Id: 2, Name: "Normal" }];
    $scope.isAdding = false;
    $scope.isDeleting = false;

    $http.get('/Users/GetUsers')
        .success(function (data) {
            $scope.userList = data;
            $.each($scope.userList, function (indx, val) {
                setUserTypeName(val);
            });
        }).error(function (response) {
            console.log(response.message);
        });

    $scope.addUser = function (form) {
        $scope.isAdding = true;
        if (form && form.$invalid)
            return;

        $http.post('/Users/AddUser', $scope.user)
            .success(function (data) {
                data = data.replace('"', '').replace('"', '');
                onSuccessAddUser(data);
            }).error(function (response) {
                console.log(response.message);
            }).finally(function () {
                $scope.isAdding = false;
            });
    }

    $scope.deleteUser = function (userId) {
        $scope.isDeleting = true;
        $http.post('/Users/DeleteUser', { "userId": userId })
                    .success(function (data) {
                        onSuccessDeleteUser(userId);
                    }).error(function (response) {
                        console.log(response.message);
                    }).finally(function () {
                        $scope.isDeleting = false;
                    });
    }

    function onSuccessAddUser(userId) {
        $scope.user.UserId = userId;
        var newUser = $.extend(true, {}, $scope.user);
        setUserTypeName(newUser);
        $scope.userList.splice(0, 0, newUser);
        $scope.user.Name = '';
        $scope.user.Type = '';
        $scope.user.UserId = '';
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
    }

    function setUserTypeName(user) {
        var userType = $.grep($scope.userTypes, function (val) {
            return val.Id === parseInt(user.Type);
        });
        if (userType != null && userType.length > 0)
            user.TypeName = userType[0].Name;
    }
}]);