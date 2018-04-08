// FiveMLicense.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "StdInc.h"


static std::string& GetLicense(const std::string& ticket)
{
	auto ticketData = Botan::base64_decode(ticket);

	// validate ticket length
	if (ticketData.size() < 20 + 4 + 128 + 4)
	{
		return {};
	}

	uint32_t length = *(uint32_t*)&ticketData[20 + 4 + 128];

	// validate full length
	if (ticketData.size() < 20 + 4 + 128 + 4 + length)
	{
		return {};
	}

	// copy extra data
	std::vector<uint8_t> extraData(length);

	if (!extraData.empty())
	{
		memcpy(&extraData[0], &ticketData[20 + 4 + 128 + 4], length);
	}

	// check the RSA signature
	uint32_t sigLength = *(uint32_t*)&ticketData[20 + 4 + 128 + 4 + length];

	if (sigLength != 128)
	{
		return {};
	}

	Botan::SHA_160 hashFunction;
	auto result = hashFunction.process(&ticketData[4], ticketData.size() - 128 - 4 - 4);

	std::vector<uint8_t> msg(result.size() + 1);
	msg[0] = 2;
	memcpy(&msg[1], &result[0], result.size());

	auto modulus = Botan::base64_decode("1DNT1go22VUAU3BON+jCfXxs7Ow9Zxwng4ARTX/vrv6I65bsSYbdBrcc"
		"w/50Fu7AJr8zy8+sXK8wUO4gx00frtA0adaGeZOeBqNq7/K3Gprv98wc"
		"ftbxWjUv75pVl9Ush5yxpBPbuYUnGR/Nh2+K3GRrIrKxWYpNSF1JZYzE"
		"+5k=");

	auto exponent = Botan::base64_decode("AQAB");

	Botan::BigInt n(modulus.data(), modulus.size());
	Botan::BigInt e(exponent.data(), exponent.size());

	auto pk = Botan::RSA_PublicKey(n, e);

	auto signer = std::make_unique<Botan::PK_Verifier>(pk, "EMSA_PKCS1(SHA-1)");

	bool valid = signer->verify_message(msg.data(), msg.size(), &ticketData[length + 4 + 4 + 128 + 20 + 4], sigLength);

	if (!valid)
	{
		trace("Connecting player: ticket RSA signature not matching\n");
		return {};
	}

	//TicketData outData;

	if (length >= 20)
	{
		std::array<uint8_t, 20> entitlementHash;
		memcpy(entitlementHash.data(), &extraData[0], entitlementHash.size());

		outData.entitlementHash = entitlementHash;
	}

	return outData;
}
