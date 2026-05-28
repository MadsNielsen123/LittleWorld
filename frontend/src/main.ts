import './style.css';

const app = document.querySelector<HTMLDivElement>('#app');

if (!app) {
  throw new Error('Missing app root element.');
}

app.innerHTML = `
  <main class="container">
    <h1>LittleWorld</h1>
    <p>Message from database:</p>
    <p id="message" class="message">Loading...</p>
  </main>
`;

const messageElement = document.querySelector<HTMLParagraphElement>('#message');

if (!messageElement) {
  throw new Error('Missing message element.');
}

try {
  const response = await fetch('/api/message');
  if (!response.ok) {
    throw new Error(`Failed to load message (${response.status}).`);
  }

  const data: { message?: string } = await response.json();
  messageElement.textContent = data.message ?? 'No message available.';
} catch {
  messageElement.textContent = 'Could not load message from backend.';
}
