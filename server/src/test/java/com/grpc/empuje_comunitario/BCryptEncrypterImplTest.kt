import com.grpc.empuje_comunitario.repository.BCryptEncrypterImpl
import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

class BCryptEncrypterImplTest {

    private val encrypter = BCryptEncrypterImpl()

    @Test
    fun `encrypt should hash and matches should verify password`() {
        val rawPassword = "mySecret123"
        val hash = encrypter.encrypt(rawPassword)

        assertNotEquals(rawPassword, hash)

        assertTrue(encrypter.matches(rawPassword, hash))

        assertFalse(encrypter.matches("wrongPassword", hash))
    }
}