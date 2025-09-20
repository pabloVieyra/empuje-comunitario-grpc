import styled from "styled-components";

// Fondo general tipo login
export const UsersPage = styled.div`
  min-height: 100vh;
  background: #171822;
  display: flex;
  align-items: center;
  justify-content: center;
`;

export const CenterBox = styled.div`
  background: linear-gradient(110deg, #f7f8fc 60%, #2456e9 100%);
  border-radius: 36px;
  box-shadow: 0 8px 48px #1a1c2512, 0 1.5px 18px #3252e710;
  width: 92vw;
  max-width: 1200px;
  min-height: 700px;
  display: flex;
  align-items: center;
  justify-content: center;
`;

export const Card = styled.div`
  background: #fff;
  border-radius: 28px;
  box-shadow: 0 4px 32px #3252e715;
  padding: 44px 38px;
  min-width: 390px;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  gap: 38px;
  align-items: center;
`;

export const Table = styled.table`
  width: 100%;
  border-spacing: 0;
  border-radius: 18px;
  background: #fff;
  box-shadow: 0 2px 18px #1a1c2512;
  overflow: hidden;
  font-size: 1.08rem;
  th, td {
    padding: 17px 12px;
    text-align: left;
    border-bottom: 1px solid #f0f1f7;
  }
  th {
    background: #f6f8fc;
    font-weight: 700;
    font-size: 1.04rem;
    color: #23244b;
  }
  tr:last-child td {
    border-bottom: none;
  }
`;

export const Btn = styled.button`
  background: #2456e9;
  color: #fff;
  border: none;
  border-radius: 14px;
  padding: 13px 0;
  width: 100%;
  font-weight: 600;
  letter-spacing: 0.025em;
  cursor: pointer;
  font-size: 1.08rem;
  box-shadow: 0 2px 12px #3252e730;
  margin-top: 10px;
  transition: 0.18s;
  &:hover { filter: brightness(1.05); }
`;

export const ModalBg = styled.div`
  position: fixed; top:0; left:0; width:100vw; height:100vh;
  display: flex; align-items: center; justify-content: center;
  z-index: 9999;
  backdrop-filter: blur(2px);
`;

export const ModalCard = styled.div`
  background: #fff;
  border-radius: 24px;
  padding: 38px 32px;
  min-width: 420px;
  max-width: 96vw;
  box-shadow: 0 4px 48px #2456e94a;
  display: flex;
  flex-direction: column;
`;

export const ModalTitle = styled.h3`
  font-size: 2rem;
  margin-bottom: 22px;
  font-weight: 700;
  color: #23244b;
  text-align: center;
`;

export const ModalFooter = styled.div`
  display: flex;
  gap: 18px;
  margin-top: 24px;
  justify-content: flex-end;
`;

export const ErrorMsg = styled.div`
  color: #ff2f2f;
  font-weight: 600;
  margin-top: 12px;
  text-align: center;
`;

export const Input = styled.input`
  width: 100%;
  padding: 13px 14px;
  border-radius: 10px;
  border: 1.5px solid #e4e8f2;
  background: #f9fbff;
  font-size: 1rem;
  margin-bottom: 18px;
  transition: border 0.16s;
  &:focus {
    border-color: #2456e9;
    outline: none;
    background: #f4f8ff;
  }
`;

export const Select = styled.select`
  width: 100%;
  padding: 13px 14px;
  border-radius: 10px;
  border: 1.5px solid #e4e8f2;
  background: #f9fbff;
  font-size: 1rem;
  margin-bottom: 18px;
  transition: border 0.16s;
  &:focus {
    border-color: #2456e9;
    outline: none;
    background: #f4f8ff;
  }
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
