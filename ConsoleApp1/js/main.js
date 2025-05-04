// Your Firebase configuration (replace with your actual config)
const firebaseConfig = {
    apiKey: "AIzaSyBr6hH97VTbQvEqQvoAXr9b9mqIo0mMsmY",
    authDomain: "notificationdemo-d4942.firebaseapp.com",
    projectId: "notificationdemo-d4942",
    messagingSenderId: "403669825090",
    appId: "1:403669825090:web:86bdac349df05937db9fdb"
};

// Initialize Firebase
firebase.initializeApp(firebaseConfig);

// Get messaging instance
const messaging = firebase.messaging();

document.getElementById('notify').addEventListener('click', () => {
    Notification.requestPermission().then(permission => {
        if (permission === 'granted') {
            messaging.getToken({ vapidKey: 'BFl3AS4UMPgmBjp1D9cJextwyOinyKrt9LH1xjko1SSCLe6bole5Yujlshmi6ciaAr76RuCq3Z9qbd8iEgO6Zjg' }).then(currentToken => {
                if (currentToken) {
                    console.log('Token:', currentToken);
                    alert('Token retrieved and ready to receive messages!');
                } else {
                    console.warn('No registration token available.');
                }
            }).catch(err => {
                console.error('An error occurred while retrieving token.', err);
            });
        }
    });
});

// Handle messages when app is in foreground
messaging.onMessage(payload => {
    console.log('Message received. ', payload);
    alert(`Notification received: ${payload.notification.title}`);
});
