const uri = 'api/Components';
let components = [];

// 1. GET
function getComponents() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayComponents(data))
        .catch(error => console.error('Не вдалося отримати компоненти:', error));
}

// 2. POST
function addComponent(event) {
    if (event) event.preventDefault(); 

    const nameTextbox = document.getElementById('add-name');
    const descTextbox = document.getElementById('add-description');
    const priceTextbox = document.getElementById('add-price');

    const item = {
        name: nameTextbox.value.trim(),
        description: descTextbox.value.trim(),
        price: parseFloat(priceTextbox.value),
        categoryId: 1,     
        manufacturerId: 1  
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Помилка сервера при створенні запису');
            }
            return response.json();
        })
        .then(() => {
            getComponents(); 
            nameTextbox.value = '';
            descTextbox.value = '';
            priceTextbox.value = '';
        })
        .catch(error => console.error('Не вдалося додати компонент:', error));
}

// 3. DELETE
function deleteComponent(id) {
    if (confirm('Ви впевнені що хочете видалити цей елемент?')) {
        fetch(`${uri}/${id}`, {
            method: 'DELETE'
        })
            .then(() => getComponents()) 
            .catch(error => console.error('Не вдалося видалити компонент:', error));
    }
}

// 4. PUT
function displayEditForm(id) {
    const item = components.find(c => c.id === id);
    if (!item) return;
    
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-description').value = item.description;
    document.getElementById('edit-price').value = item.price;
    
    document.getElementById('editFormContainer').style.display = 'block';
}

// 5. PUT
function updateComponent(event) {
    if (event) event.preventDefault(); 

    const itemId = parseInt(document.getElementById('edit-id').value, 10);
    
    const item = {
        id: itemId,
        name: document.getElementById('edit-name').value.trim(),
        description: document.getElementById('edit-description').value.trim(),
        price: parseFloat(document.getElementById('edit-price').value),
        categoryId: 1, 
        manufacturerId: 1
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Помилка при оновленні запису');
            }
            getComponents();
            closeInput();
        })
        .catch(error => console.error('Не вдалося оновити компонент:', error));
}

function closeInput() {
    document.getElementById('editFormContainer').style.display = 'none';
}

function _displayComponents(data) {
    const tBody = document.getElementById('components-table-body');
    if (!tBody) return;
    tBody.innerHTML = '';

    data.forEach(item => {
        let tr = tBody.insertRow();
        
        let tdName = tr.insertCell(0);
        tdName.appendChild(document.createTextNode(item.name));
        
        let tdDesc = tr.insertCell(1);
        tdDesc.appendChild(document.createTextNode(item.description || ''));
        
        let tdPrice = tr.insertCell(2);
        tdPrice.appendChild(document.createTextNode(`${item.price.toFixed(2)} грн`));
        
        let tdActions = tr.insertCell(3);
        
        let editButton = document.createElement('button');
        editButton.innerText = 'Редагувати';
        editButton.className = 'btn-edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);
        tdActions.appendChild(editButton);
        
        let deleteButton = document.createElement('button');
        deleteButton.innerText = 'Видалити';
        deleteButton.className = 'btn-delete';
        deleteButton.setAttribute('onclick', `deleteComponent(${item.id})`);
        tdActions.appendChild(deleteButton);
    });

    components = data;
} 