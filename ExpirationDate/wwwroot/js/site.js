document.querySelector('.custom-file-label').addEventListener('click', function (e) {
    e.preventDefault();
    document.querySelector('.custom-file-input').click();
});

document.querySelector('.custom-file-input').addEventListener('change', function () {
    var fileName = this.files[0] ? this.files[0].name : '@localizer["CreatePage.SelectFile"]';
    document.querySelector('.custom-file-label').textContent = fileName;
});
