
const firebaseConfig = {
    apiKey: window.firebaseSettings.apiKey,
    authDomain: window.firebaseSettings.authDomain,
    projectId: window.firebaseSettings.projectId,
    messagingSenderId: window.firebaseSettings.messagingSenderId,
    appId: window.firebaseSettings.appId
};
const vapidKey = window.firebaseSettings.vapidKey;
firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();
navigator.serviceWorker.register('/firebase-messaging-sw.js')
    .then((registration) => {
        messaging.getToken({
            vapidKey: vapidKey,
            serviceWorkerRegistration: registration
        }).then((token) => {
            if (token) {
                console.log("FCM Token:", token);
                fetch('/home/savetoken', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ token: token })
                });
                console.log("Notification permission granted.");
            } else {
                console.warn("No token retrieved.");
            }
        }).catch((err) => {
            console.error("Token error", err);
        });

        messaging.onMessage((payload) => {
            console.log("Foreground message:", payload);
            alert(`Notification: ${payload.notification.title}`);
        });
    })
    .catch(err => {
        console.error("Service worker registration failed", err);
    });

document.getElementById('notifyBtn').addEventListener('click', () => {
    Notification.requestPermission().then(permission => {
        if (permission !== 'granted') {
            alert("Permission denied.");
        }
    });
});
