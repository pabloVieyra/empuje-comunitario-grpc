import React from "react";
import { Link } from "react-router";
import styled from "styled-components";

const HomeBg = styled.div`
  background: #171822;
  min-height: 100vh;
  display: flex; align-items: center; justify-content: center;
`;

const CenterBox = styled.div`
  background: linear-gradient(110deg, #f7f8fc 60%, #2456e9 100%);
  border-radius: 36px;
  box-shadow: 0 8px 48px #1a1c2512;
  width: 92vw;
  max-width: 1200px;
  min-height: 700px;
  display: flex; align-items: center; justify-content: center;
`;

const Card = styled.div`
  background: #fff;
  border-radius: 28px;
  box-shadow: 0 4px 32px #3252e715;
  padding: 96px 68px;
  min-width: 390px;
  max-width: 420px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: 38px;
  align-items: center;
`;

const Title = styled.h1`
  font-size: 2.7rem;
  font-weight: 800;
  margin-bottom: 22px;
  color: #23244b;
  text-align: center;
`;

const NavBtn = styled(Link)`
  background: #2456e9;
  color: #fff;
  font-weight: 700;
  border-radius: 14px;
  padding: 18px 46px;
  font-size: 1.15rem;
  text-decoration: none;
  box-shadow: 0 2px 12px #3252e730;
  transition: .18s;
  &:hover { filter: brightness(1.07); }
`;

const Home: React.FC = () => (
  <HomeBg>
    <CenterBox>
      <Card>
        <Title>Panel de Gestión</Title>
        <NavBtn to="/usuarios">Ir a ABM de Usuarios</NavBtn>
        <NavBtn to="/donaciones">ABM de Donaciones</NavBtn>
        <NavBtn to="/eventos">ABM de Eventos</NavBtn>
      </Card>
    </CenterBox>
  </HomeBg>
);

export default Home;