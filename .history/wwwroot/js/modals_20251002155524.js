function openRolesModal(userId, userRole) {
    // Показываем userId и роль в тексте
    document.getElementById('modalUserIdText').textContent = userId;
    document.getElementById('modalUserRoleText').textContent = userRole;

    // Устанавливаем значение в скрытый инпут формы для отправки
    document.getElementById('modalUserId').value = userId;

    // Выбираем роль в селекте
    var roleSelect = document.getElementById('roleSelect');
    if (roleSelect) {
        roleSelect.value = userRole;
    }

    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show();
}