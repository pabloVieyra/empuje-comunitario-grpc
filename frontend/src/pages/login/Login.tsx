import { LoginCard } from "./components/LoginCard";
import { LoginForm } from "./components/LoginForm";
import { LoginHeader } from "./components/LoginHeader";
import { LoginLayout } from "./components/LoginLayout";

const Login: React.FC = () => (
  <LoginLayout>
    <LoginCard>
      <LoginHeader />
      <LoginForm />
    </LoginCard>
  </LoginLayout>
);

export default Login;