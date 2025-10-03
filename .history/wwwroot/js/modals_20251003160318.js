function openRolesModal(userId, currentRole) {
   
   document.getElementById('modalUserIdDisplay').textContent = userId; // Обновляем отображение userId
   document.getElementById('modalUserId').value = userId; // Передаем в скрытое поле
   var roleSelect = document.getElementById('roleSelect'); // Устанавливаем текущую роль выбранной в селекте
   roleSelect.value = currentRole;

   // Открываем модалку, если используете Bootstrap 5
   var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
   myModal.show();
}