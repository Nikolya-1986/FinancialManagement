function openRolesModal(userId, currentRole) {
   // Обновляем отображение userId
   document.getElementById('modalUserIdDisplay').textContent = userId;
   // Передаем в скрытое поле
   document.getElementById('modalUserId').value = userId;
   // Устанавливаем текущую роль выбранной в селекте
   var roleSelect = document.getElementById('roleSelect');
   roleSelect.value = currentRole;

   // Открываем модалку, если используете Bootstrap 5
   var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
   myModal.show();
}