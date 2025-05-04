// Give Firebase access to service worker
importScripts('https://www.gstatic.com/firebasejs/9.6.10/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.6.10/firebase-messaging-compat.js');

firebase.initializeApp({
    apiKey: "AIzaSyDHVonnuuEZ4pCTQWi8EtQ041zmATTcDAM",
    authDomain: "hackeranitchat.firebaseapp.com",
    projectId: "hackeranitchat",
    messagingSenderId: "1093843318673",
    appId: "1:1093843318673:web:88ddb34a099aa98b978c30"
});

const messaging = firebase.messaging();

// Handle background messages
messaging.onBackgroundMessage(payload => {
    console.log('[firebase-messaging-sw.js] Received background message ', payload);
    self.registration.showNotification(payload.notification.title, {
        body: payload.notification.body,
        icon: '/firebase-logo.png'
    });
});
