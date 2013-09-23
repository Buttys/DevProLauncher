--Ｎｏ．３９希望皇ホープ･ルーツ
function c80700048.initial_effect(c)
	--xyz summon
	aux.AddXyzProcedure(c,aux.XyzFilterFunction(c,1),2)
	c:EnableReviveLimit()
	--disable attack
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80700048,0))
	e1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_TRIGGER_O)
	e1:SetRange(LOCATION_MZONE)
	e1:SetCode(EVENT_ATTACK_ANNOUNCE)
	e1:SetCost(c80700048.atkcost)
	e1:SetOperation(c80700048.atkop)
	c:RegisterEffect(e1)
end
function c80700048.atkcost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return e:GetHandler():CheckRemoveOverlayCard(tp,1,REASON_COST) end
	e:GetHandler():RemoveOverlayCard(tp,1,1,REASON_COST)
end
function c80700048.atkop(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	local tc=Duel.GetAttackTarget()
	Duel.NegateAttack()
	if tc:IsType(TYPE_XYZ) then
		local e1=Effect.CreateEffect(c)
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_UPDATE_ATTACK)
		e1:SetValue(tc:GetRank()*500)
		e1:SetReset(RESET_EVENT+0x1ff0000)
		c:RegisterEffect(e1)
	end
end
