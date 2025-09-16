import styled from "styled-components";

export const OuterWrapper = styled.div`
  height: 100vh;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  background: #10101e;
`;

export const Layout = styled.div`
  display: flex;
  width: 1200px;
  max-width: 98vw;
  min-height: 80vh;
  align-items: stretch;
  background: transparent;
  border-radius: 38px;
  box-shadow: 0 12px 60px #0004;
  overflow: hidden;
  @media (max-width: 900px) {
    flex-direction: column;
    width: 98vw;
    min-height: 98vh;
    border-radius: 18px;
    box-shadow: 0 4px 28px #0002;
  }
`;

export const LeftPanel = styled.div`
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  background: #f8fafd;
  min-height: 80vh;
  @media (max-width: 900px) {
    min-height: unset;
    padding: 32px 0;
  }
`;

export const RightPanel = styled.div`
  flex: 1;
  background: linear-gradient(120deg, #1738a4 0%, #3d5ce8 70%, #d8e7ff 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  min-height: 80vh;
  overflow: hidden;
  @media (max-width: 900px) {
    display: none;
  }
`;

export const Visual = styled.div`
  width: 85%;
  height: 85%;
  background: url("/login-bg-art.png"), linear-gradient(120deg, #1738a4 0%, #3d5ce8 70%, #d8e7ff 100%);
  background-size: cover;
  background-position: center;
  border-radius: 32px;
  box-shadow: 0 0 64px #1738a4cc;
  opacity: 0.85;
  filter: blur(0.5px);
`;

export const Card = styled.div`
  background: #fff;
  border-radius: 32px;
  box-shadow: 0 8px 64px #0002;
  width: 410px;
  max-width: 96vw;
  padding: 48px 40px;
  display: flex;
  flex-direction: column;
  gap: 32px;
  position: relative;
  @media (max-width: 600px) {
    padding: 32px 12px;
    border-radius: 18px;
    width: 100%;
    gap: 22px;
  }
`;

export const Logo = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    & > div {
        display: flex;
        align-items: center;
        justify-content: center;
    }

    & > span {
        text-align: center;
    }
`;

export const Icon = styled.div`
  width: 36px;
  height: 36px;
  border-radius: 12px;
  background: #1738a4cc;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #fff;
  font-weight: bold;
  font-size: 24px;
`;

export const Title = styled.h1`
  color: black;
  font-size: 1.7rem;
  font-weight: 600;
  margin: 0;
  @media (max-width: 600px) {
    font-size: 1.35rem;
  }
`;

export const Subtitle = styled.p`
  font-size: 1rem;
  color: #555;
  margin-bottom: 0;
  @media (max-width: 600px) {
    font-size: 0.93rem;
  }
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 10px;
`;

export const InputWrapper = styled.div`
  flex-direction: column;
  display: flex;
  position: relative;
  margin-bottom: 8px;
`;

export const Label = styled.label`
  font-size: 0.97rem;
  font-weight: 500;
  color: #222;
  margin-bottom: 5px;
  display: block;
`;

export const ErrorMsg = styled.div`
  color: #e44;
  font-size: 1rem;
  margin-bottom: 6px;
  text-align: left;
  padding-left: 2px;
`;

export const Btn = styled.button`
  width: 100%;
  background: #3d5ce8;
  color: #fff;
  font-size: 1.09rem;
  font-weight: 600;
  border: none;
  border-radius: 12px;
  padding: 14px 0;
  margin-top: 6px;
  cursor: pointer;
  box-shadow: 0 4px 24px #3d5ce868;
  transition: background 0.2s;
  &:hover {
    background: #1738a4;
  }
`;