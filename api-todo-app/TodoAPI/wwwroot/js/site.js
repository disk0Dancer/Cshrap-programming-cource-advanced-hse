// const uri = '/api/ToDo';
// let todos = [];
// let categories = [];
//
// function getItems() {
//     fetch(uri)
//         .then(response => response.json())
//         .then(data => _displayItems(data))
//         .catch(error => console.error('Unable to get items.', error));
// }
//
// function addItem() {
//     const addNameTextbox = document.getElementById('add-name');
//     const addCategoryDropdown = document.getElementById('add-category');
//
//     console.log(addCategoryDropdown.value)
//     const item = {
//         isComplete: false,
//         name: addNameTextbox.value.trim(),
//         categoryId: addCategoryDropdown.value
//     };
//
//     fetch(uri, {
//         method: 'POST',
//         headers: {
//             'Accept': 'application/json',
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify(item)
//     })
//         .then(response => response.json())
//         .then(() => {
//             getItems();
//             addNameTextbox.value = '';
//         })
//         .catch(error => console.error('Unable to add item.', error));
// }
//
// function deleteItem(id) {
//     fetch(`${uri}/${id}`, {
//         method: 'DELETE'
//     })
//         .then(() => getItems())
//         .catch(error => console.error('Unable to delete item.', error));
// }
//
// function displayeditForm(id) {
//     const item = todos.find(item => item.id === id);
//
//     document.getElementById('edit-name').value = item.name;
//     document.getElementById('edit-id').value = item.id;
//     document.getElementById('edit-isComplete').checked = item.isComplete;
//
//     const editCategoryDropdown = document.getElementById('edit-category');
//     editCategoryDropdown.value = item.categoryId;
//
//     document.getElementById('edit-form').style.display = 'block';
// }
//
// function updateItem() {
//     const itemId = document.getElementById('edit-id').value;
//     const item = {
//         id: parseInt(itemId, 10),
//         isComplete: document.getElementById('edit-isComplete').checked,
//         name: document.getElementById('edit-name').value.trim(),
//         categoryId: document.getElementById('edit-category').value
//     };
//
//     fetch(`${uri}/${itemId}`, {
//         method: 'PUT',
//         headers: {
//             'Accept': 'application/json',
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify(item)
//     })
//         .then(() => getItems())
//         .catch(error => console.error('Unable to update item.', error));
//
//     closeInput();
//
//     return false;
// }
//
// function closeInput() {
//     document.getElementById('edit-form').style.display = 'none';
// }
//
// function _displayItems(data) {
//     const tBody = document.getElementById('todos');
//     tBody.innerHTML = '';
//
//     data.forEach(item => {
//         let isCompleteCheckbox = document.createElement('input');
//         isCompleteCheckbox.type = 'checkbox';
//         isCompleteCheckbox.readOnly = true;
//         isCompleteCheckbox.checked = Boolean(item.isComplete);
//
//         let editButton = document.createElement('button');
//         editButton.innerText = 'Edit';
//         editButton.addEventListener('click', () => displayeditForm(item.id));
//
//         let deleteButton = document.createElement('button');
//         deleteButton.innerText = 'Delete';
//         deleteButton.addEventListener('click', () => deleteItem(item.id));
//
//         let tr = tBody.insertRow();
//
//         let td1 = tr.insertCell(0);
//         td1.appendChild(isCompleteCheckbox);
//
//         let td2 = tr.insertCell(1);
//         let textNode = document.createTextNode(item.name);
//         td2.appendChild(textNode);
//
//         let td3 = tr.insertCell(2);
//         let catNode = document.createTextNode(item.categoryId);
//         td3.appendChild(catNode);
//
//         let td4 = tr.insertCell(3);
//         td4.appendChild(editButton);
//
//         let td5 = tr.insertCell(4);
//         td5.appendChild(deleteButton);
//     });
//
//     todos = data;
// }
//
// function getCategories() {
//     fetch('/api/Category')
//         .then(response => response.json())
//         .then(data => {
//             categories = data;
//             populateDropdowns();
//         })
//         .catch(error => console.error('Unable to get categories.', error));
// }
//
// function populateDropdowns() {
//     const addCategoryDropdown = document.getElementById('add-category');
//     const editCategoryDropdown = document.getElementById('edit-category');
//
//     categories.forEach(category => {
//         const addOption = document.createElement('option');
//         addOption.value = category.id;
//         addOption.text = category.title;
//         addCategoryDropdown.appendChild(addOption);
//
//         const editOption = document.createElement('option');
//         editOption.value = category.id;
//         editOption.text = category.title;
//         editCategoryDropdown.appendChild(editOption);
//     });
// }
//
// document.getElementById('add-form').addEventListener('submit', (e) => {
//     e.preventDefault();
//     addItem();
// });
//
// document.getElementById('edit-form').addEventListener('submit', (e) => {
//     e.preventDefault();
//     updateItem();
//     return false;
// });
//
// getItems();
// getCategories();

const todoApiUrl = '/api/ToDo';
const categoryApiUrl = '/api/Category';
let todos = [];
let categories = [];

function getItems() {
    fetch(todoApiUrl)
        .then(response => response.json())
        .then(data => displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addCategoryDropdown = document.getElementById('add-category-dropdown');

    const item = {
        isComplete: false,
        name: addNameTextbox.value.trim(),
        categoryId: addCategoryDropdown.value
    };

    fetch(todoApiUrl, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${todoApiUrl}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isComplete').checked = item.isComplete;

    const editCategoryDropdown = document.getElementById('edit-category-dropdown');
    editCategoryDropdown.value = item.categoryId;

    document.getElementById('edit-form').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isComplete: document.getElementById('edit-isComplete').checked,
        name: document.getElementById('edit-name').value.trim(),
        categoryId: document.getElementById('edit-category-dropdown').value
    };

    fetch(`${todoApiUrl}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeEditForm();

    return false;
}

function closeEditForm() {
    document.getElementById('edit-form').style.display = 'none';
}

function displayItems(data) {
    const todosTableBody = document.getElementById('todos');
    todosTableBody.innerHTML = '';

    data.forEach(item => {
        const isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = Boolean(item.isComplete);

        const editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.addEventListener('click', () => displayEditForm(item.id));

        const deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.addEventListener('click', () => deleteItem(item.id));

        const tr = todosTableBody.insertRow();

        const td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        const td2 = tr.insertCell(1);
        const textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        const td3 = tr.insertCell(2);
        const catNode = document.createTextNode(getCategoryName(item.categoryId));
        td3.appendChild(catNode);

        const td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        const td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });

    todos = data;
}

function getCategories() {
    fetch(categoryApiUrl)
        .then(response => response.json())
        .then(data => {
            categories = data;
            populateCategoryDropdowns();
            displayCategories(data);
        })
        .catch(error => console.error('Unable to get categories.', error));
}

function addCategory() {
    const categoryNameTextbox = document.getElementById('add-category-name');
    const category = {
        title: categoryNameTextbox.value.trim()
    };

    fetch(categoryApiUrl, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
        .then(response => response.json())
        .then(() => {
            getCategories();
            categoryNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add category.', error));
}

function deleteCategory(id) {
    fetch(`${categoryApiUrl}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getCategories())
        .catch(error => console.error('Unable to delete category.', error));
}

function displayCategoryEditForm(id) {
    const category = categories.find(category => category.id === id);

    document.getElementById('edit-category-name').value = category.title;
    document.getElementById('edit-category-id').value = category.id;

    document.getElementById('edit-category-form').style.display = 'block';
}

function updateCategory() {
    const categoryId = document.getElementById('edit-category-id').value;
    const category = {
        id: parseInt(categoryId, 10),
        title: document.getElementById('edit-category-name').value.trim()
    };

    fetch(`${categoryApiUrl}/${categoryId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
        .then(() => getCategories())
        .catch(error => console.error('Unable to update category.', error));

    closeCategoryEditForm();

    return false;
}

function closeCategoryEditForm() {
    document.getElementById('edit-category-form').style.display = 'none';
}

function displayCategories(data) {
    const categoriesTableBody = document.getElementById('categories');
    categoriesTableBody.innerHTML = '';

    data.forEach(category => {
        const editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.addEventListener('click', () => displayCategoryEditForm(category.id));

        const deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.addEventListener('click', () => deleteCategory(category.id));

        const tr = categoriesTableBody.insertRow();

        const td1 = tr.insertCell(0);
        const textNode = document.createTextNode(category.title);
        td1.appendChild(textNode);

        const td2 = tr.insertCell(1);
        td2.appendChild(editButton);

        const td3 = tr.insertCell(2);
        td3.appendChild(deleteButton);
    });
}

function getCategoryName(categoryId) {
    const category = categories.find(category => category.id === categoryId);
    return category ? category.title : 'Unknown';
}

function populateCategoryDropdowns() {
    const addCategoryDropdown = document.getElementById('add-category-dropdown');
    const editCategoryDropdown = document.getElementById('edit-category-dropdown');

    addCategoryDropdown.innerHTML = '';
    editCategoryDropdown.innerHTML = '';

    categories.forEach(category => {
        const addOption = document.createElement('option');
        addOption.value = category.id;
        addOption.text = category.title;
        addCategoryDropdown.appendChild(addOption);

        const editOption = document.createElement('option');
        editOption.value = category.id;
        editOption.text = category.title;
        editCategoryDropdown.appendChild(editOption);
    });
}

document.getElementById('add-form').addEventListener('submit', (e) => {
    e.preventDefault();
    addItem();
});

document.getElementById('edit-form').addEventListener('submit', (e) => {
    e.preventDefault();
    updateItem();
    return false;
});

document.getElementById('add-category-form').addEventListener('submit', (e) => {
    e.preventDefault();
    addCategory();
});

document.getElementById('edit-category-form').addEventListener('submit', (e) => {
    e.preventDefault();
    updateCategory();
    return false;
});

getItems();
getCategories();
