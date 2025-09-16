import styled from "styled-components";
import { Logo, Icon, Title, Subtitle } from "../styles";

export const LoginHeader: React.FC = () => (
  <Container>
    <Logo>
      <Icon>👨‍💻</Icon>
    </Logo>
    <Title>¡Bienvenido!</Title>
    <Subtitle>Ingresa tus datos para continuar.</Subtitle>
  </Container>
);

const Container = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`;