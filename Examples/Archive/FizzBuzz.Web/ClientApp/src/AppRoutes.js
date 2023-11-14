import { PreDefinedRules } from "./components/PreDefinedRules";
import { Home } from "./components/Home";
import { CustomRules } from './components/CustomRules';

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/custom-rules',
    element: <CustomRules />
  },
  {
    path: '/predefined-rules',
    element: <PreDefinedRules />
  }
];

export default AppRoutes;
