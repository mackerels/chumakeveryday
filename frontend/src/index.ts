import 'styles/chumak.css';
import 'whatwg-fetch';
import 'core-js';
import {Client} from './client/client';

let client = new Client();
client.init().then(() => console.log('Initialized client.'));