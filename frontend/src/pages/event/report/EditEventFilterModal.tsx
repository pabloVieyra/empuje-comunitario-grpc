import React, { useState } from "react";
import { Card, Btn } from "../../users/styles";
import { Box, Text, Input, Flex } from "@chakra-ui/react";

const EditEventFilterModal = ({ filter, onClose, onEdit }) => {
  const [name, setName] = useState(filter.name);

  return (
    <Box
      position="fixed"
      top="0" left="0"
      w="100vw" h="100vh"
      bg="rgba(0,0,0,0.35)"
      zIndex={999}
      display="flex"
      alignItems="center"
      justifyContent="center"
    >
      <Card style={{
        borderRadius: "36px", minWidth: 340, maxWidth: 420, padding: "38px 32px",
        boxShadow: "0 8px 48px #1a1c2512"
      }}>
        <Text fontWeight={700} fontSize="1.3rem" color="#23244b" mb={4} textAlign="center">
          Editar filtro
        </Text>
        <Input
          value={name}
          onChange={e => setName(e.target.value)}
          size="md"
          mb={6}
          bg="#222"
          color="#fff"
          borderRadius={8}
          border="none"
        />
        <Flex gap={4} justify="center">
          <Btn onClick={() => {
            onEdit({ ...filter, name });
            onClose();
          }} style={{ background: "#2456e9", color: "#fff", fontWeight: 600 }}>
            Guardar cambios
          </Btn>
          <Btn onClick={onClose} style={{ background: "#eee", color: "#23244b", fontWeight: 600 }}>
            Cancelar
          </Btn>
        </Flex>
      </Card>
    </Box>
  );
};

export default EditEventFilterModal;