document.addEventListener("DOMContentLoaded", () => {
    const path = window.location.pathname.toLowerCase();

    if (!path.startsWith("/identity/account/manage")) {
        return;
    }

    const textMap = new Map([
        ["Manage your account", "Управление на профила"],
        ["Change your account settings", "Настройки на акаунта"],
        ["Profile", "Профил"],
        ["Manage Email", "Имейл"],
        ["Email", "Имейл"],
        ["Password", "Парола"],
        ["Change Password", "Смяна на парола"],
        ["Change password", "Смяна на парола"],
        ["Set password", "Задаване на парола"],
        ["Two-factor authentication (2FA)", "Двуфакторна защита"],
        ["Two-factor authentication", "Двуфакторна защита"],
        ["Personal data", "Лични данни"],
        ["Personal Data", "Лични данни"],
        ["Delete Personal Data", "Изтриване на лични данни"],
        ["External Logins", "Външни входове"],
        ["Username", "Потребителско име"],
        ["Phone number", "Телефон"],
        ["Save", "Запази"],
        ["Current password", "Текуща парола"],
        ["New password", "Нова парола"],
        ["Confirm new password", "Потвърди новата парола"],
        ["Update password", "Обнови паролата"],
        ["Your email is unchanged.", "Имейлът не е променен."],
        ["New email", "Нов имейл"],
        ["Change email", "Смени имейл"],
        ["Send verification email", "Изпрати имейл за потвърждение"],
        ["Download", "Изтегли"],
        ["Delete", "Изтрий"],
        ["Delete personal data", "Изтриване на лични данни"],
        ["External logins", "Външни входове"],
        ["Authenticator app", "Authenticator приложение"],
        ["Recovery codes", "Кодове за възстановяване"],
        ["Disable 2FA", "Изключи 2FA"],
        ["Reset authenticator key", "Нулирай authenticator ключа"]
    ]);

    const translateNode = (node) => {
        const text = node.textContent.trim();
        const translated = textMap.get(text);

        if (translated) {
            node.textContent = translated;
        }
    };

    document
        .querySelectorAll("main h1, main h2, main h3, main label, main button, main a.nav-link, main th, main td, main p, main strong")
        .forEach(translateNode);

    const titlePrefix = document.querySelector("main h3")?.textContent?.trim()
        || document.querySelector("main h1")?.textContent?.trim();

    if (titlePrefix) {
        document.title = `${titlePrefix} - AutoZone`;
    }
});
