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

export const LoginForm: React.FC = () => {
  const [values, setValues] = useState({ userOrEmail: "", password: "" });
  const { loading, error, login, setError } = useLogin();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setValues((v) => ({ ...v, [e.target.name]: e.target.value }));
    setError(null);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!values.userOrEmail || !values.password) {
      setError("Completa ambos campos para ingresar.");
      return;
    }
    const success = await login(values);
    if (success) {
      // Redirigir al home, ejemplo con react-router-dom v6
      window.location.href = "/";
    }
  };

  return (
    <Form onSubmit={handleSubmit} autoComplete="off">
      <InputWrapper>
        <Label htmlFor="userOrEmail">Usuario o Email</Label>
        <Input
          color={"black"}
          id="userOrEmail"
          name="userOrEmail"
          value={values.userOrEmail}
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