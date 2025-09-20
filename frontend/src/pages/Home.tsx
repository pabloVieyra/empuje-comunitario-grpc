import React from "react";
import { Link } from "react-router";
import styled from "styled-components";

const HomePage = styled.div`
  background: linear-gradient(120deg, #f4f6fb 60%, #1a1c25 100%);
  min-height: 100vh;
  display: flex; flex-direction: column; align-items: center; justify-content: center;
`;

const Title = styled.h1`
  font-size: 3rem;
  font-weight: 800;
  margin-bottom: 36px;
  color: #23244b;
`;

const NavBtn = styled(Link)`
  background: linear-gradient(90deg, #3252e7 0%, #4f66fa 100%);
  color: #fff;
  font-weight: 700;
  border-radius: 14px;
  padding: 18px 46px;
  font-size: 1.15rem;
  text-decoration: none;
  transition: .18s;
  &:hover { filter: brightness(1.08); }
`;

const Home: React.FC = () => (
  <HomePage>
    <Title>Panel de Gestión</Title>
    <NavBtn to="/usuarios">Ir a ABM de Usuarios</NavBtn>
  </HomePage>
);

export default Home;