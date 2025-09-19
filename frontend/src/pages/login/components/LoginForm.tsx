import { useState } from "react";
import {
  Form,
  InputWrapper,
  Label,
  ErrorMsg,
  Btn,
} from "../styles";
import { Input } from "@chakra-ui/react/input";
import { useLogin } from "../useLogin";
import { useNavigate } from "react-router";


export const LoginForm: React.FC = () => {
  const [values, setValues] = useState({ usernameOrEmail: "", password: "" });
  const { loading, error, login, setError } = useLogin();
  const navigate = useNavigate();


  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setValues((v) => ({ ...v, [e.target.name]: e.target.value }));
    setError(null);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!values.usernameOrEmail || !values.password) {
      setError("Completa ambos campos para ingresar.");
      return;
    }
    const success = await login(values);
    if (success) {
        navigate("/");
    }
  };

  return (
    <Form onSubmit={handleSubmit} autoComplete="off">
      <InputWrapper>
        <Label htmlFor="usernameOrEmail">Usuario o Email</Label>
        <Input
          color={"black"}
          id="usernameOrEmail"
          name="usernameOrEmail"
          value={values.usernameOrEmail}
          onChange={handleChange}
          autoFocus
        />
      </InputWrapper>
      <InputWrapper>
        <Label htmlFor="password">Contraseña</Label>
        <Input
          color={"black"}
          id="password"
          name="password"
          type="password"
          value={values.password}
          onChange={handleChange}
        />
      </InputWrapper>
      {error && <ErrorMsg>{error}</ErrorMsg>}
      <Btn type="submit" disabled={loading}>
        {loading ? "Ingresando..." : "Login"}
      </Btn>
    </Form>
  );
};