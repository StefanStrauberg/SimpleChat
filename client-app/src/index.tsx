import ReactDOM from 'react-dom/client';
import 'semantic-ui-css/semantic.min.css'
import './app/layout/style.css';
import App from './app/layout/App';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
    <App />
);

reportWebVitals();
