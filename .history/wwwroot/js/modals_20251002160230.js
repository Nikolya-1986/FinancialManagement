document.addEventListener('DOMContentLoaded', function () {
    window.openRolesModal = function(userId, userRole) {
        // Заполняем данные в модалке
        document.getElementById('modalUserIdText').textContent = userId;
        document.getElementById('modalUserRoleText').textContent = userRole ?? 'None';
        document.getElementById('modalUserId').value = userId;

        // Устанавливаем значение select на текущую роль пользователя если есть
        const roleSelect = document.getElementById('roleSelect');
        if (roleSelect) {
            roleSelect.value = userRole || roleSelect.options[0].value;
        }

        // Инициализируем модальное окно и показываем его
        var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
        myModal.show();
    };
});