shopingListApp.controller('categoryController', ['$scope', '$http', function ($scope, $http) {
    $scope.categoryList = [];
    $scope.isAdding = false;
    $scope.isDeleting = false;
    $scope.isDuplicateCategory = false;
    $scope.isUpdating = false;

    $http.get('/Category/GetCategories')
        .success(function (data) {
            $scope.categoryList = data;
            $.each($scope.categoryList, function (indx, val) {
                val.IsUpdating = false;
                val.HideControls = false;
            });
        }).error(function (response) {
            console.log(response.message);
        });

    $scope.addCategory = function (form) {
        $scope.isAdding = true;
        if (form && form.$invalid)
            return;

        if (isDuplicateCategory($scope.category)) {
            $scope.isDuplicateCategory = true;
            return;
        }

        alertBox.hide();
        $http.post('/Category/AddCategoryAsync', $scope.category)
            .success(function (data) {
                data = data.replace('"', '').replace('"', '');
                onSuccessAddCategory(data);
            }).error(function (response) {
                alertBox.showErrorMessage(response.message);
                console.log(response.message);
            }).finally(function () {
                $scope.isAdding = false;
            });
    }

    $scope.deleteCategory = function (categoryId) {
        alertBox.hide();
        $scope.isDeleting = true;
        $http.post('/Category/DeleteCategoryAsync', { "categoryId": categoryId })
                    .success(function () {
                        onSuccessDeleteCategory(categoryId);
                    }).error(function (response) {
                        alertBox.showErrorMessage(response.message);
                        console.log(response.message);
                    }).finally(function () {
                        $scope.isDeleting = false;
                    });
    }

    $scope.checkDuplicateCategory = function (category) {
        $scope.isDuplicateCategory = isDuplicateCategory(category);
    }

    function onSuccessAddCategory(categoryId) {
        $scope.category.CategoryId = categoryId;
        var newCategory = $.extend(true, {}, $scope.category);
        $scope.categoryList.splice(0, 0, newCategory);
        $scope.category.Name = '';
        $scope.category.CategoryId = '';
        alertBox.showSuccessMessage("Successfully added the category.");
    }

    function onSuccessDeleteCategory(categoryId) {
        var index = -1;
        $.each($scope.categoryList, function (indx, val) {
            if (val.CategoryId === categoryId) {
                index = indx;
            }
        });

        if (index >= 0)
            $scope.categoryList.splice(index, 1);
        alertBox.showSuccessMessage("Successfully deleted the category.");
    }

    function isDuplicateCategory(category) {
        if (category.Name == null || category.Name === '')
            return false;

        var duplicateCategory = $.grep($scope.categoryList, function (val) {
            return val.Name.toUpperCase() === category.Name.toUpperCase()
            && (category.CategoryId == undefined || val.CategoryId !== category.CategoryId);
        });

        if (duplicateCategory != null && duplicateCategory.length > 0)
            return true;
        return false;
    }

    $scope.editCategory = function (category) {
        $.each($scope.categoryList, function (indx, val) {
            if (val.CategoryId === category.CategoryId) {
                val.IsUpdating = true;
            } else {
                val.HideControls = true;
            }
        });
        $scope.isUpdating = true;
    }

    $scope.updateCategory = function (form, category) {

        var invalid = false;

        if (category.Name == undefined || category.Name === '') {
            form.updateName.$error.required = true;
            invalid = true;
        } else {
            form.updateName.$error.required = false;
        }

        if (invalid || $scope.isDuplicateCategory)
            return;

        alertBox.hide();
        $http.post('/Category/UpdateCategoryAsync', category)
            .success(function () {
                alertBox.showSuccessMessage("Successfully updated the category.");
            }).error(function (response) {
                alertBox.showErrorMessage(response.message);
                console.log(response.message);
            }).finally(function () {
                $.each($scope.categoryList, function (indx, val) {
                    if (val.CategoryId === category.CategoryId) {
                        val.IsUpdating = false;
                    } else {
                        val.HideControls = false;
                    }
                });
                $scope.isUpdating = false;
            });
    }

    $scope.cancelUpdate = function (category) {
        $.each($scope.categoryList, function (indx, val) {
            if (val.CategoryId === category.CategoryId) {
                val.IsUpdating = false;
            } else {
                val.HideControls = false;
            }
        });
        $scope.isUpdating = false;
    }
}]);