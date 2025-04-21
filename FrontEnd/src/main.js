import { createApp } from 'vue';
import App from './App.vue';
import MisaEmployee from '../src/helpers/resource';
import store from '../src/store/store'
import router from '../src/router.js'
import { format_date } from './utils/FormatNumber.js';
import components from './utils/ComponentGlobal.js'
const app = createApp(App);
Object.entries(components).forEach(([name, component]) => app.component(name, component))
app.mixin({
    methods: {
        format_date
    },
})
app.use(store)
app.use(router)
app.config.globalProperties.MisaEmployee = MisaEmployee;
app.mount('#app');
